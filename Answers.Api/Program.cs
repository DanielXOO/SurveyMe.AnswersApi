using System.Text.Json.Serialization;
using Answers.Api.Consumers;
using Answers.Api.Converters;
using Answers.Api.Extensions;
using Answers.Api.Handlers;
using Answers.Data;
using Answers.Data.Abstracts;
using Answers.Data.Refit;
using Answers.Domain.Answers.AutoMapper.Profiles;
using Answers.Domain.Answers.Commands;
using Answers.Domain.Answers.Validators.Commands;
using Answers.Domain.Personalities.AutoMapper.Profiles;
using Answers.Domain.Validation;
using FluentValidation;
using IdentityServer4.AccessTokenValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Refit;
using SurveyMe.Common.Logging;
using SurveyMe.QueueModels;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureLogging(logBuilder =>
{
    logBuilder.AddLogger();
    logBuilder.AddFile(builder.Configuration.GetSection("Serilog:FileLogging"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AnswersDbContext>(options
    => options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new AnswerJsonConverter());
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.JsonSerializerOptions.Converters.Add(new AnswerResultJsonConverter());
    });

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddMaps(typeof(Program).Assembly);
});

builder.Services.AddMediatR(typeof(AddAnswerCommand).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(AddAnswerCommandValidator).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IAnswersUnitOfWork, AnswersUnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SurveysConsumer>();
    
    x.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", cfg =>
        {
            cfg.Username("guest");
            cfg.Password("guest");
        });
        
        config.ReceiveEndpoint("survey-answers-queue", e =>
        {
            e.Bind<SurveyQueueModel>();
            e.ConfigureConsumer<SurveysConsumer>(context);
        });
    });
});

builder.Services.AddRefitClient<ISurveyPersonOptionsApi>()
    .ConfigureHttpClient(c =>
    {
        var stringUrl = builder.Configuration.GetConnectionString("SurveyPersonOptionsApi");
        c.BaseAddress = new Uri(stringUrl);
    })
    .AddHttpMessageHandler<AuthorizeHandler>();

builder.Services.AddRefitClient<IPersonsApi>()
    .ConfigureHttpClient(c =>
    {
        var stringUrl = builder.Configuration.GetConnectionString("PersonsApi");
        c.BaseAddress = new Uri(stringUrl);
    })
    .AddHttpMessageHandler<AuthorizeHandler>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<AuthorizeHandler>();

builder.Services.AddAutoMapper(configuration =>
{
    configuration.AddProfile(new QueueModelsProfile());
    configuration.AddProfile(new PersonalityProfile());
});

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
    .AddIdentityServerAuthentication(options =>
    {
        options.Authority = "https://localhost:7179";
        options.RequireHttpsMetadata = false;
        options.ApiName = "SurveyMeApi";
        options.ApiSecret = "api_secret";
        options.JwtValidationClockSkew = TimeSpan.FromSeconds(1);
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.Services.CreateDbIfNotExists();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
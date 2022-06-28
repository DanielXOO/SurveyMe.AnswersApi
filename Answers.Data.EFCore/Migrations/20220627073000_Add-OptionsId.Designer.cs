﻿// <auto-generated />
using System;
using Answers.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Answers.Data.Migrations
{
    [DbContext(typeof(AnswersDbContext))]
    [Migration("20220627073000_Add-OptionsId")]
    partial class AddOptionsId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Answers.Models.Answers.BaseQuestionAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("QuestionType")
                        .HasColumnType("int");

                    b.Property<Guid>("SurveyAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyAnswerId");

                    b.ToTable("BaseQuestionAnswer");

                    b.HasDiscriminator<int>("QuestionType");
                });

            modelBuilder.Entity("Answers.Models.Answers.OptionQuestionAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CheckboxAnswerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CheckboxAnswerId");

                    b.ToTable("OptionQuestionAnswer");
                });

            modelBuilder.Entity("Answers.Models.Answers.SurveyAnswer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserPersonalityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyAnswer");
                });

            modelBuilder.Entity("Answers.Models.Options.Option", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("QuestionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Option");
                });

            modelBuilder.Entity("Answers.Models.Questions.Question", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("Answers.Models.Surveys.Survey", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SurveyOptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Survey");
                });

            modelBuilder.Entity("Answers.Models.SurveysOptions.SurveyPersonOptions", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("RequireAges")
                        .HasColumnType("bit");

                    b.Property<bool>("RequireFirstName")
                        .HasColumnType("bit");

                    b.Property<bool>("RequireGender")
                        .HasColumnType("bit");

                    b.Property<bool>("RequireSecondName")
                        .HasColumnType("bit");

                    b.Property<Guid>("SurveyId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SurveyId");

                    b.ToTable("SurveyPersonOptions");
                });

            modelBuilder.Entity("Answers.Models.Answers.CheckboxQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Answers.Models.Answers.FileQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.Property<Guid>("FileId")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("Answers.Models.Answers.RadioQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.Property<Guid>("OptionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Answers.Models.Answers.RateQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue(4);
                });

            modelBuilder.Entity("Answers.Models.Answers.ScaleQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.Property<double>("Scale")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue(5);
                });

            modelBuilder.Entity("Answers.Models.Answers.TextQuestionAnswer", b =>
                {
                    b.HasBaseType("Answers.Models.Answers.BaseQuestionAnswer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Answers.Models.Answers.BaseQuestionAnswer", b =>
                {
                    b.HasOne("Answers.Models.Answers.SurveyAnswer", "SurveyAnswer")
                        .WithMany("QuestionsAnswers")
                        .HasForeignKey("SurveyAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SurveyAnswer");
                });

            modelBuilder.Entity("Answers.Models.Answers.OptionQuestionAnswer", b =>
                {
                    b.HasOne("Answers.Models.Answers.CheckboxQuestionAnswer", "CheckboxQuestionAnswer")
                        .WithMany("Options")
                        .HasForeignKey("CheckboxAnswerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CheckboxQuestionAnswer");
                });

            modelBuilder.Entity("Answers.Models.Answers.SurveyAnswer", b =>
                {
                    b.HasOne("Answers.Models.Surveys.Survey", "Survey")
                        .WithMany("Answers")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Answers.Models.Options.Option", b =>
                {
                    b.HasOne("Answers.Models.Questions.Question", "Question")
                        .WithMany("Options")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Answers.Models.Questions.Question", b =>
                {
                    b.HasOne("Answers.Models.Surveys.Survey", "Survey")
                        .WithMany("Questions")
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Answers.Models.SurveysOptions.SurveyPersonOptions", b =>
                {
                    b.HasOne("Answers.Models.Surveys.Survey", "Survey")
                        .WithMany()
                        .HasForeignKey("SurveyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Survey");
                });

            modelBuilder.Entity("Answers.Models.Answers.SurveyAnswer", b =>
                {
                    b.Navigation("QuestionsAnswers");
                });

            modelBuilder.Entity("Answers.Models.Questions.Question", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Answers.Models.Surveys.Survey", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Answers.Models.Answers.CheckboxQuestionAnswer", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
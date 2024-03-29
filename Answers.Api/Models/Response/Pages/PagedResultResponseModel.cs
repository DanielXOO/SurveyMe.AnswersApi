﻿namespace Answers.Api.Models.Response.Pages;

public sealed class PagedResultResponseModel<TModel>
{
    public IReadOnlyCollection<TModel> Items { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }
    
    public int TotalItems { get; set; }
}
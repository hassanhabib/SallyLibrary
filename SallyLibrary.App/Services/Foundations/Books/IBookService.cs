﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using SallyLibrary.App.Models.Books;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public interface IBookService
    {
        Book AddBook(Book book);
    }
}

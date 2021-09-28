// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using System.Collections.Generic;
using SallyLibrary.App.Models.Books;

namespace SallyLibrary.App.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        Book InsertBook(Book book);
        List<Book> SelectAllBooks();
        Book SelectBookById(Guid bookId);
        Book UpdateBook(Book book);
        Book DeleteBook(Book book);
    }
}

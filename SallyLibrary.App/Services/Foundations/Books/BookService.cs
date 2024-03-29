﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using SallyLibrary.App.Brokers.Loggings;
using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public partial class BookService : IBookService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public BookService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.storageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public Book AddBook(Book book) =>
        TryCatch(() =>
        {
            ValidateBook(book);

            return this.storageBroker.InsertBook(book);
        });

        public Book ModifyBook(Book book) =>
        TryCatch(() =>
        {
            ValidateBook(book);

            return this.storageBroker.UpdateBook(book);
        });

        public Book RetrieveBookById(Guid id) =>
        TryCatch(() =>
         {
             ValidateBookById(id);

             Book maybeBook =
                this.storageBroker.SelectBookById(id);

             ValidateStorageBook(id, maybeBook);

             return maybeBook;
        });
        
        public Book RemoveBookById(Guid id) =>
        TryCatch(() =>
        {
            ValidateBookById(id);

            Book maybeBook =
                this.storageBroker.SelectBookById(id);

            ValidateStorageBook(id, maybeBook);

            Book deleteBook =
                this.storageBroker.DeleteBook(maybeBook);

            return deleteBook;
        });
    }
}

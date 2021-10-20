// ---------------------------------------------------------------
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

        public Book AddBook(Book book)
        {
            try
            {
                ValidateBook(book);

                return this.storageBroker.InsertBook(book);
            }
            catch (NullBookException nullBookException)
            {
                var bookValidationException = new BookValidationException(nullBookException);
                this.loggingBroker.LogError(bookValidationException);

                throw bookValidationException;
            }
            catch (InvalidBookException invalidBookException)
            {
                var bookValidationException = new BookValidationException(invalidBookException);
                this.loggingBroker.LogError(bookValidationException);
                throw bookValidationException;
            }
            catch (Exception exception)
            {
                var failedBookServiceException = 
                    new FailedBookServiceException(exception);
                
                var bookServiceException = 
                    new BookServiceException(failedBookServiceException);

                this.loggingBroker.LogError(bookServiceException);

                throw bookServiceException;
            }
        }

        public Book RetrieveBookById(Guid id)
        {
            ValidateBookById(id);

            Book maybeBook =
                this.storageBroker.SelectBookById(id);

            ValidateStorageBook(id, maybeBook);

            return maybeBook;
        }

        public Book ModifyBook(Book book)
        {
            ValidateBook(book);

            return this.storageBroker.UpdateBook(book);
        }

        public Book RemoveBookById(Guid id)
        {
            ValidateBookById(id);

            Book maybeBook =
                this.storageBroker.SelectBookById(id);

            ValidateStorageBook(id, maybeBook);

            Book deleteBook =
                this.storageBroker.DeleteBook(maybeBook);

            return deleteBook;
        }
    }
}

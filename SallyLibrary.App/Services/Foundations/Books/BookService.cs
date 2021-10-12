// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public partial class BookService : IBookService
    {
        private readonly IStorageBroker storageBroker;

        public BookService(IStorageBroker storageBroker) =>
            this.storageBroker = storageBroker;

        public Book AddBook(Book book)
        {
            try
            {
                ValidateBook(book);

                return this.storageBroker.InsertBook(book);
            }
            catch (NullBookException nullBookException)
            {
                throw nullBookException;
            }
            catch (InvalidBookException invalidBookException)
            {
                throw invalidBookException;
            }
            catch (Exception exception)
            {
                throw new BookServiceException(exception);
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

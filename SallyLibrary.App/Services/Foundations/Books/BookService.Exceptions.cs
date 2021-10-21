using System;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;
using Xeptions;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public partial class BookService
    {
        private delegate Book ReturningBookFunction();

        private Book TryCatch(ReturningBookFunction returningBookFunction)
        {
            try
            {
                return returningBookFunction();
            }
            catch (NullBookException nullBookException)
            {
                throw CreateAndLogValidationException(nullBookException);
            }
            catch (InvalidBookException invalidBookException)
            {
                throw CreateAndLogValidationException(invalidBookException);
            }
            catch (Exception exception)
            {
                var failedBookServiceException =
                    new FailedBookServiceException(exception);

                throw CreateAndLogServiceException(failedBookServiceException);
            }
        }

        private BookValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var bookValidationException = new BookValidationException(xeption);
            this.loggingBroker.LogError(bookValidationException);

            return bookValidationException;
        }

        private BookServiceException CreateAndLogServiceException(Xeption xeption)
        {
            var bookServiceException = new BookServiceException(xeption);
            this.loggingBroker.LogError(bookServiceException);

            return bookServiceException;
        }
    }
}

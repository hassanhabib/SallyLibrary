using System;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class BookServiceException : Exception
    {
        public BookServiceException(Exception innerException)
            : base("Book service error occurred, contact support.", innerException)
        { }
    }
}

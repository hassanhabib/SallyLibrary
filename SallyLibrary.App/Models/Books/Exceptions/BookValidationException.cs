using System;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class BookValidationException : Exception
    {
        public BookValidationException(Exception innerException)
            : base("Invalid input, contact support.", innerException)
        { }
    }
}

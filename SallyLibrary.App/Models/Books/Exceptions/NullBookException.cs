using System;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class NullBookException : Exception
    {
        public NullBookException()
            : base("Book is null.")
        { }
    }
}

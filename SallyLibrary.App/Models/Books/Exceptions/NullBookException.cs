using System;
using Xeptions;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class NullBookException : Xeption
    {
        public NullBookException()
            : base("Book is null.")
        { }
    }
}

// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using Xeptions;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class InvalidBookException : Xeption
    {
        public InvalidBookException()
            : base("Invalid book. Correct the errors and try again.")
        { }
    }
}

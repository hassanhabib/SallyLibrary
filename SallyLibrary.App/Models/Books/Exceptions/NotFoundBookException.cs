// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Xeptions;

namespace SallyLibrary.App.Models.Books.Exceptions
{
    public class NotFoundBookException : Xeption
    {
        public NotFoundBookException(Guid id)
            : base($"Book with id: {id} is not found.") { }
    }
}

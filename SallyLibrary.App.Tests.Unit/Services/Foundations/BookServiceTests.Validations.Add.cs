// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;
using SallyLibrary.App.Services.Foundations.Books;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldThrowNullBookExceptionOnAddIfBookIsNull()
        {
            // given
            Book nullBook = null;

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(nullBook);

            // then
            Assert.Throws<NullBookException>(addBookAction);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
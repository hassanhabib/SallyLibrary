// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldThrowServiceExceptionOnAddIfServiceErrorOccurs()
        {
            // given
            var someBook = CreateRandomBook();
            var serviceException = new Exception();

            var expectedBookServiceException =
                new BookServiceException(serviceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBook(It.IsAny<Book>()))
                    .Throws(serviceException);

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(someBook);

            // then
            Assert.Throws<BookServiceException>(addBookAction);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
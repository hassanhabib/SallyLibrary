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

            var failedBookServiceException =
                new FailedBookServiceException(serviceException);

            var expectedBookServiceException =
                new BookServiceException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBook(It.IsAny<Book>()))
                    .Throws(serviceException);

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(someBook);

            // then
            BookServiceException actualBookServiceException = 
                Assert.Throws<BookServiceException>(addBookAction);

            SameExceptionAs(actualBookServiceException, expectedBookServiceException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
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
        public void ShouldThrowServiceExceptionOnRetrieveByIdIfServiceErrorOccursAndLogIt()
        {
            // given
            Guid someId = Guid.NewGuid();
            var serviceException = new Exception();

            var failedBookServiceException =
                new FailedBookServiceException(serviceException);

            var expectedBookServiceException =
                new BookServiceException(failedBookServiceException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookById(It.IsAny<Guid>()))
                    .Throws(serviceException);

            // when
            Action reretrieveBookByIdAction = () =>
                this.bookService.RetrieveBookById(someId);

            // then 
            Assert.Throws<BookServiceException>(reretrieveBookByIdAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookServiceException))),
                        Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
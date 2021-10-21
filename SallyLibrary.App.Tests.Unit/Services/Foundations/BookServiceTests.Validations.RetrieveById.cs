// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalidAndLogIt()
        {
            // given
            Guid invalidBookId = Guid.Empty;

            var InvalidBookException =
                new InvalidBookException();

            InvalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            var expectedBookValidationException =
                new BookValidationException(InvalidBookException);

            // when
            Action retireveBookByIdAction = () =>
                this.bookService.RetrieveBookById(invalidBookId);

            // then
            Assert.Throws<BookValidationException>(retireveBookByIdAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedBookValidationException))),
                        Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionOnRetrieveByIdIfBookIsNotFound()
        {
            // given
            Guid someId = Guid.NewGuid();
            Book noBook = null;

            var notFoundBookException =
                new NotFoundBookException(someId);

            var expectedBookValidationException =
              new BookValidationException(notFoundBookException);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookById(It.IsAny<Guid>()))
                    .Returns(noBook);

            // when
            Action retrieveBookByIdAction = () =>
                this.bookService.RetrieveBookById(someId);

            // then
            Assert.Throws<BookValidationException>(retrieveBookByIdAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
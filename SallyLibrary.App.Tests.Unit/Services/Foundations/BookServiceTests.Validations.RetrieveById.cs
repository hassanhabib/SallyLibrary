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
        public void ShouldThrowValidationExceptionOnRetrieveByIdIfIdIsInvalid()
        {
            // given
            Guid invalidBookId = Guid.Empty;

            var expectedInvalidBookException =
                new InvalidBookException();

            expectedInvalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            // when
            Action retireveBookByIdAction = () =>
                this.bookService.RetrieveBookById(invalidBookId);

            // then
            InvalidBookException actualInvalidBookException =
                Assert.Throws<InvalidBookException>(retireveBookByIdAction);

            actualInvalidBookException.Data.Should().BeEquivalentTo(
                expectedInvalidBookException.Data);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowNotFoundExceptionOnRetrieveByIdIfBookIsNotFound()
        {
            // given
            Guid someId = Guid.NewGuid();
            Book noBook = null;

            var notFoundBookException =
                new NotFoundBookException(someId);

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookById(It.IsAny<Guid>()))
                    .Returns(noBook);

            // when
            Action retrieveBookByIdAction = () =>
                this.bookService.RetrieveBookById(someId);

            // then
            Assert.Throws<NotFoundBookException>(retrieveBookByIdAction);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
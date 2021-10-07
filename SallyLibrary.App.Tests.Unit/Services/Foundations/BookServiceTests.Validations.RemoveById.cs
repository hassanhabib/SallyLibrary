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
        public void ShouldThrowValidaitonExceptionOnRemoveByIdIfIdIsInvalid()
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
                this.bookService.RemoveBookById(invalidBookId);

            // then
            InvalidBookException actualInvalidBookException =
                Assert.Throws<InvalidBookException>(retireveBookByIdAction);

            actualInvalidBookException.Data.Should().BeEquivalentTo(
                expectedInvalidBookException.Data);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(It.IsAny<Guid>()),
                    Times.Never);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}

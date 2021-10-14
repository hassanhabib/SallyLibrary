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
        public void ShouldThrowNullBookExceptionOnAddIfBookIsNull()
        {
            // given
            Book nullBook = null;

            var nullBookException = new NullBookException();

            var expectedBookValidationException =
                new BookValidationException(nullBookException);

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(nullBook);

            // then
            BookValidationException actualNullBookValisationException =
                Assert.Throws<BookValidationException>(addBookAction);

            SameExceptionAs(actualNullBookValisationException, expectedBookValidationException)
                .Should().BeTrue();

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowInvalidBookExceptionIfBookIsInvalid()
        {
            // given
            var invalidBook = new Book();

            var expectedInvalidBookException =
                new InvalidBookException();

            var expectedBookValidationException =
                new BookValidationException(expectedInvalidBookException);

            expectedInvalidBookException.AddData(
                key: nameof(Book.Id),
                values: "Id is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.Title),
                values: "Text is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.Description),
                values: "Text is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.Author),
                values: "Text is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.Price),
                values: "Price is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.PageCount),
                values: "Number is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.ReleaseDate),
                values: "Date is required");

            expectedInvalidBookException.AddData(
                key: nameof(Book.ISBN),
                values: "Text is required");

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(invalidBook);

            // then
            BookValidationException actualInvalidBookException =
                Assert.Throws<BookValidationException>(addBookAction);


            SameExceptionAs(actualInvalidBookException, expectedBookValidationException)
                .Should().BeTrue();

            actualInvalidBookException.Data.Should().BeEquivalentTo(
                expectedInvalidBookException.Data);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
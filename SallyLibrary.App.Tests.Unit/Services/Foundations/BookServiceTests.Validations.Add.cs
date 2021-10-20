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
            Assert.Throws<BookValidationException>(addBookAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public void ShouldThrowInvalidBookExceptionIfBookIsInvalid()
        {
            // given
            var invalidBook = new Book();

            var expectedInvalidBookException =
                new InvalidBookException();

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

            var expectedBookValidationException =
                new BookValidationException(expectedInvalidBookException);

            // when
            Action addBookAction = () =>
                this.bookService.AddBook(invalidBook);

            // then
            Assert.Throws<BookValidationException>(addBookAction);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(expectedBookValidationException))),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}
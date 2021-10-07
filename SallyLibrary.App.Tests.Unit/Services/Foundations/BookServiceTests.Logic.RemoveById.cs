// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using FluentAssertions;
using Moq;
using SallyLibrary.App.Models.Books;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldRemoveBook()
        {
            //given
            Book randomBook = CreateRandomBook();
            Book inputBook = randomBook;
            Guid inputBookId = randomBook.Id;
            Book storageBook = randomBook;
            Book expectedBook = storageBook;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookById(inputBookId))
                    .Returns(storageBook);

            this.storageBrokerMock.Setup(broker =>
                 broker.DeleteBook(inputBook))
                     .Returns(storageBook);

            //when
            Book actualBook =
                this.bookService.RemoveBookById(inputBookId);

            //then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(inputBookId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteBook(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
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
using SallyLibrary.App.Services.Foundations.Books;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldRemoveBookById()
        {
            // given
            Book randomBook = new Book();
            Guid inputBookId = randomBook.Id;
            Book inputbook = randomBook;
            Book storageBook = inputbook;
            Book expectedBook = storageBook;

            this.storageBrokerMock.Setup(broker =>
                broker.SelectBookById(inputBookId))
                .Returns(inputbook);

            this.storageBrokerMock.Setup(broker =>
                broker.DeleteBook(inputbook))
                    .Returns(storageBook);

            //when
            Book actualBook = this.bookService.RemoveBookById(inputBookId);

            //then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.SelectBookById(inputBookId),
                    Times.Once);

            this.storageBrokerMock.Verify(broker =>
                broker.DeleteBook(inputbook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
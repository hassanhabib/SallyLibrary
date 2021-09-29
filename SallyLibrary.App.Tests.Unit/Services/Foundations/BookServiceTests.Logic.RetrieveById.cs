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

            //given
            //Guid inputBookId = Guid.NewGuid();

            Book randomBook = new Book();
            Guid inputBookId = randomBook.Id;

            Book inputBook = randomBook;
            Book storageBook = inputBook;
            Book expectedBook = storageBook;

            this.storageBrokerMock.Setup(broker =>broker.SelectBookById(inputBookId)).Returns(inputBook);
            this.storageBrokerMock.Setup(broker => broker.DeleteBook(inputBook)).Returns(storageBook);

            //when
            Book actualbook = this.bookService.RemoveBookById(inputBookId);

            // then

            actualbook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker => broker.SelectBookById(inputBookId), Times.Once);
            this.storageBrokerMock.Verify(broker => broker.DeleteBook(inputBook), Times.Once);
            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
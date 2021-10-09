// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using FluentAssertions;
using Force.DeepCloner;
using Moq;
using SallyLibrary.App.Models.Books;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldAddBook()
        {
            // given
            Book randomBook = CreateRandomBook();
            Book inputBook = randomBook;
            Book storageBook = inputBook;
            Book expectedBook = storageBook.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertBook(inputBook))
                    .Returns(storageBook);

            // when
            Book actualBook =
                this.bookService.AddBook(inputBook);

            // then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.InsertBook(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
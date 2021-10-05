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
        public void ShouldModifyBook()
        {
            // given
            var randomBook = new Book
            {
                Id = Guid.NewGuid(),
                Title = "AnotherThing",
                Description = "Something",
                Author = "something",
                Price = 2,
                ISBN = "Something",
                PageCount = 2,
                ReleaseDate = new DateTime(2008, 5, 1, 8, 30, 0),
                InStock = true,
            };

            Book inputBook = randomBook;
            Book storageBook = inputBook;
            Book expectedBook = storageBook;

            this.storageBrokerMock.Setup(broker =>
                broker.UpdateBook(inputBook))
                    .Returns(storageBook);

            //when
            Book actualBook = this.bookService.ModifyBook(inputBook);

            // then
            actualBook.Should().BeEquivalentTo(expectedBook);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBook(inputBook),
                    Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
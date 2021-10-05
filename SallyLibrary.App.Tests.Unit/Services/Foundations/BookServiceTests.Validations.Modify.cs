﻿// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Moq;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;
using Xunit;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        [Fact]
        public void ShouldThrowNullBookExceptionOnModifyIfBookIsNull()
        {
            // given
            Book nullBook = null;

            // when
            Action modifyBookAction = () =>
                this.bookService.ModifyBook(nullBook);

            // then
            Assert.Throws<NullBookException>(modifyBookAction);

            this.storageBrokerMock.Verify(broker =>
                broker.UpdateBook(It.IsAny<Book>()),
                    Times.Never);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Moq;
using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Services.Foundations.Books;
using Tynamix.ObjectFiller;
using System.Linq.Expressions;

namespace SallyLibrary.App.Tests.Unit.Services.Foundations
{
    public partial class BookServiceTests
    {
        private readonly Mock<IStorageBroker> storageBrokerMock;
        private readonly IBookService bookService;

        public BookServiceTests()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.bookService = new BookService(
                storageBroker: this.storageBrokerMock.Object);
        }

        private static Book CreateRandomBook() =>
            CreateBookFiller().Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static bool SameExceptionAs(Exception actualException, Exception expectedException)
        {
            return 
                actualException.Message == expectedException.Message 
                && actualException.InnerException.Message == expectedException.InnerException.Message;
        }

        private static Filler<Book> CreateBookFiller()
        {
            var filler = new Filler<Book>();

            filler.Setup()
                .OnType<DateTimeOffset>().Use(GetRandomDateTimeOffset())
                .OnType<bool>().Use(true);

            return filler;
        }
    }
}

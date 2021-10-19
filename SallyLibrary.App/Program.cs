// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;
using Microsoft.Extensions.Logging;
using SallyLibrary.App.Brokers.Loggings;
using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Services.Foundations.Books;

var loggerFactory = new LoggerFactory();
var logger = loggerFactory.CreateLogger("Whatever");
var storageBroker = new StorageBroker();
var loggingBroker = new LoggingBroker(logger);
var bookService = new BookService(storageBroker, loggingBroker);

var book = new Book();
//bookService.RetrieveBookById(Guid.NewGuid());

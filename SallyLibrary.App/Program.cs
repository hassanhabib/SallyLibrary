// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Services.Foundations.Books;

var storageBroker = new StorageBroker();
var bookService = new BookService(storageBroker);
var book = new Book();
bookService.AddBook(null);
bookService.ModifyBook(null);
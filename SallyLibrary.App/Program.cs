using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Services.Foundations.Books;

var storageBroker = new StorageBroker();
var bookService = new BookService(storageBroker);
var book = new Book();

bookService.AddBook(book);

// test

using SallyLibrary.App.Brokers.Storages;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Services.Foundations.Books;

var storageBroker = new StorageBroker();
var bookService = new BookService(storageBroker);
var book = new Book();

bookService.AddBook(null);
bookService.ModifyBook(null);

// measure the commitment between the clients, the services and the dependencies
// based on the causes of the exceptions and it's ratios in your system

// 100 requests 
// 10 exceptions occurred
// data is bad (OK)         90!! we need to talk to our customers.
// service is bad (NOT OK)
// dependency is bad (OK)

// service - nobody knows how to use it?

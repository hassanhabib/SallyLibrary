using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public partial class BookService
    {
        private static void ValidateBook(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
            }
        }
    }
}

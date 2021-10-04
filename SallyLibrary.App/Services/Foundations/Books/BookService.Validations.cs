using System;
using SallyLibrary.App.Models.Books;
using SallyLibrary.App.Models.Books.Exceptions;

namespace SallyLibrary.App.Services.Foundations.Books
{
    public partial class BookService
    {
        private static void ValidateBook(Book book)
        {
            ValidateBookIsNotNull(book);

            Validate(
                (Rule: IsInvalid(book.Id), Parameter: nameof(Book.Id)),
                (Rule: IsInvalid(book.Title), Parameter: nameof(Book.Title)),
                (Rule: IsInvalid(book.Description), Parameter: nameof(Book.Description)),
                (Rule: IsInvalid(book.Author), Parameter: nameof(Book.Author)),
                (Rule: IsInvalid(book.Price), Parameter: nameof(Book.Price)),
                (Rule: IsInvalid(book.ISBN), Parameter: nameof(Book.ISBN)));
        }

        private static void ValidateBookIsNotNull(Book book)
        {
            if (book is null)
            {
                throw new NullBookException();
            }
        }

        private static dynamic IsInvalid(Guid id) => new
        {
            Condition = id == Guid.Empty,
            Message = "Id is required"
        };

        private static dynamic IsInvalid(string text) => new
        {
            Condition = String.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static dynamic IsInvalid(double Number) => new
        {
            Condition = Number == default,
            Message = "Price is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidBookException = new InvalidBookException();

            foreach((dynamic rule, string parameter) in validations)
            {
                if(rule.Condition)
                {
                    invalidBookException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidBookException.ThrowIfContainsErrors();
        }
    }
}

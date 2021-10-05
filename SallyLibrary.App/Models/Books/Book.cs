// ---------------------------------------------------------------
// Copyright (c) Coalition of the Good-Hearted Engineers
// FREE TO USE AS LONG AS SOFTWARE FUNDS ARE DONATED TO THE POOR
// ---------------------------------------------------------------

using System;

namespace SallyLibrary.App.Models.Books
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public double Price { get; set; }
        public int PageCount { get; set; }
        public DateTimeOffset ReleaseDate { get; set; }
        public bool InStock { get; set; }
        public string ISBN { get; set; }
    }
}

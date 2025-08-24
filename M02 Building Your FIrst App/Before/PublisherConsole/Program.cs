﻿using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

// Following is useful for demos only
using PubContext context = new();
context.Database.EnsureCreated();
Console.WriteLine("Database created (if it did not exist).");

//AddAuthor();
//GetAuthors();

//void AddAuthor()
//{
//    var author = new Author
//    {
//        FirstName = "Chris",
//        LastName = "Taylor"
//    };
//    context.Authors.Add(author);
//    context.SaveChanges();
//}

//void GetAuthors()
//{
//    var authors = context.Authors.ToList();
//    foreach (var author in authors)
//    {
//        Console.WriteLine($"{author.Id}: {author.FirstName} {author.LastName}");
//    }
//}

AddAuthorWithBook();
GetAuthorsWithBooks();

void AddAuthorWithBook()
{
    var author = new Author { FirstName = "Julie", LastName = "Lerman" };
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework",
        PublishDate = new DateOnly(2009, 1, 1)
    });
    author.Books.Add(new Book
    {
        Title = "Programming Entity Framework 2nd Ed",
        PublishDate = new DateOnly(2010, 8, 1)
    });
    using var context = new PubContext();
    context.Authors.Add(author);
    context.SaveChanges();
}
void GetAuthorsWithBooks()
{
    using var context = new PubContext();
    var authors = context.Authors.Include(a => a.Books).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
        foreach (var book in author.Books)
        {
            Console.WriteLine(book.Title);
        }
    }
}
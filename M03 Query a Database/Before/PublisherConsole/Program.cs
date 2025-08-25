using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

//GetAuthors();

//void GetAuthors()
//{
//    using var context = new PubContext();
//    var authors = context.Authors.ToList();
//    foreach (var author in authors)
//    {
//        Console.WriteLine(author.FirstName + " " + author.LastName);
//    }
//}

using PubContext _context = new();

// QueryFilters("Taylor");

// AddSomeMoreAuthors();
// QueryFiltersLike();

// SkipAnTakeAuthors();

QueryAggregate();

void AddSomeMoreAuthors()
{
    _context.Authors.Add(new Author { FirstName = "Rhoda", LastName = "Lerman" });
    _context.Authors.Add(new Author { FirstName = "Don", LastName = "Jones" });
    _context.Authors.Add(new Author { FirstName = "Jim", LastName = "Christopher" });
    _context.Authors.Add(new Author { FirstName = "Steven", LastName = "Haunts" });
    _context.SaveChanges();
}

void QueryFilters(string lastName)
{
    var authors = _context.Authors
        .Where(a => a.LastName == lastName).ToList();

    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void QueryFiltersLike()
{
    var filter = "L%";
    var authors = _context.Authors
        .Where(a => EF.Functions.Like(a.LastName, filter)).ToList();
    foreach (var author in authors)
    {
        Console.WriteLine(author.FirstName + " " + author.LastName);
    }
}

void SkipAnTakeAuthors()
{
    var groupSize = 2;
    for (var i = 0; i < 4; i++)
    {
        var authors = _context.Authors
            .OrderBy(a => a.LastName)
            .Skip(i * groupSize)
            .Take(groupSize)
            .ToList();
        Console.WriteLine($"Group {i}:");
        foreach (var author in authors)
        {
            Console.WriteLine($"  - {author.FirstName} {author.LastName}");
        }
    }
}

void QueryAggregate()
{
    var author = _context.Authors
        .AsNoTracking()
        .FirstOrDefault(a => a.LastName == "Martin");
    Console.WriteLine(author is null ? "No author found" : $"{author.FirstName} {author.LastName}");
}

QueryTrackingBehavior.
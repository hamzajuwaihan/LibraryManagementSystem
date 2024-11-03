using System.Text.Json.Serialization;
using ManagementLibrarySystem.Domain.Exceptions.Book;
using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;
/// <summary>
/// Book Domain
/// </summary>
public class Book : Entity
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsBorrowed { get; set; }
    public DateTime? BorrowedDate { get; set; }
    public Guid? BorrowedBy { get; set; }
    public Guid LibraryId { get; set; }

    [JsonIgnore]
    public Library LibraryAssociated { get; set; } = null!;

    /// <summary>
    /// Private Cosntructor for EF Core
    /// </summary>
    /// <param name="id"></param>
    public Book(Guid id) : base(id) { }

    /// <summary>
    /// general constructor with JsonConstructor attribute for unit testing
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="isBorrowed"></param>
    /// <param name="borrowedDate"></param>
    /// <param name="borrowedBy"></param>
    [JsonConstructor]
    public Book(Guid id, string title, string author, bool isBorrowed, DateTime? borrowedDate, Guid? borrowedBy) : base(id)
    {

        Title = title;
        Author = author;
        IsBorrowed = isBorrowed;
        if (borrowedDate != null) BorrowedDate = borrowedDate;
        if (borrowedBy == Guid.Empty) BorrowedBy = borrowedBy;
    }
    /// <summary>
    /// Update book method
    /// </summary>
    /// <param name="title"></param>
    /// <param name="author"></param>
    /// <param name="isBorrowed"></param>
    /// <param name="borrowedDate"></param>
    /// <param name="borrowedBy"></param>
    public void Update(string title, string author, bool isBorrowed, DateTime? borrowedDate, Guid? borrowedBy)
    {
        Title = title;
        Author = author;
        IsBorrowed = isBorrowed;
        BorrowedDate = borrowedDate;
        BorrowedBy = borrowedBy;
    }

    public void Borrow(Guid? borrowedBy)
    {
        BorrowedDate = DateTime.UtcNow;

        if (IsBorrowed == true) throw new BookAlreadyBorrowedException();

        IsBorrowed = true;

        BorrowedBy = borrowedBy;
    }

    public void Return()
    {
        if (IsBorrowed == false) throw new BookIsNotCurrentlyBorrowedException();

        IsBorrowed = false;
        BorrowedDate = null;
        BorrowedBy = null;
    }
}

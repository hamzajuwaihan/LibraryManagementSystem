using System.Text.Json.Serialization;
using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;

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

    public Book(Guid BookID, string Title, string Author, bool IsBorrowed, DateTime? BorrowedDate, Guid? BorrowedBy) : base(BookID)
    {

        this.Title = Title;
        this.Author = Author;
        this.IsBorrowed = IsBorrowed;
        if (BorrowedDate != null) this.BorrowedDate = BorrowedDate;
        if (BorrowedBy == Guid.Empty) this.BorrowedBy = BorrowedBy;
    }

    public void Update(string title, string author, bool isBorrowed, DateTime? borrowedDate, Guid? borrowedBy)
    {
        Title = title;
        Author = author;
        IsBorrowed = isBorrowed;
        BorrowedDate = borrowedDate;
        BorrowedBy = borrowedBy;
    }
}

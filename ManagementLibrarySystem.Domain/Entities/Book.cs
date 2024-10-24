using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;

public class Book : Entity
{
    public required string Title { get; set; }
    public required string Author { get; set; }
    public bool IsBorrowed { get; protected set; }
    public DateTime? BorrowedDate { get; private set; }
    public Guid? BorrowedBy { get; private set; }
    public Member Member { get; set; } = null!;
    public Guid LibraryId { get; set; }
    /// <summary>
    /// Private Cosntructor for EF Core
    /// </summary>
    /// <param name="id"></param>
    public Book(Guid id) : base(Guid.Empty) { }

    public Book(Guid BookID, string Title, string Author, bool IsBorrowed, DateTime? BorrowedDate, Guid? BorrowedBy, Member? Member) : base(BookID)
    {

        this.Title = Title;
        this.Author = Author;
        this.IsBorrowed = IsBorrowed;
        if (BorrowedDate != null) this.BorrowedDate = BorrowedDate;
        if (Member != null) this.Member = Member;
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

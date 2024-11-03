namespace ManagementLibrarySystem.Domain.Exceptions.Book;

public class BookIsNotCurrentlyBorrowedException : Exception
{
    public BookIsNotCurrentlyBorrowedException() : base("Book is not currently Borrowed.") { }
}

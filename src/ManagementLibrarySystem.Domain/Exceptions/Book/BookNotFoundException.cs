namespace ManagementLibrarySystem.Domain.Exceptions.Book;

public class BookNotFoundException : Exception
{
    public BookNotFoundException() : base("The specified book was not found.") { }

}

using System;

namespace ManagementLibrarySystem.Domain.Exceptions.Book;

public class BookAlreadyBorrowedException : Exception
{
    public BookAlreadyBorrowedException() : base("The specified book is already borrowed.")
    {

    }
}

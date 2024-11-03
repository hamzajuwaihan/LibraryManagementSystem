namespace ManagementLibrarySystem.Domain.Exceptions.Library;

public class LibraryDoesNotHaveThisMemberException : Exception
{
    public LibraryDoesNotHaveThisMemberException() : base("The member is not registered in the provided library.") { }
}

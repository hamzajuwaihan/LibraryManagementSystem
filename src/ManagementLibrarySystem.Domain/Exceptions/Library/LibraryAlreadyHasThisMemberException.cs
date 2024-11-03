namespace ManagementLibrarySystem.Domain.Exceptions.Library;

public class LibraryAlreadyHasThisMemberException : Exception
{
    public LibraryAlreadyHasThisMemberException() : base("Library already has this member registered.") { }
}

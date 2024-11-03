namespace ManagementLibrarySystem.Domain.Exceptions.Library;

public class LibraryNotFoundException : Exception
{
    public LibraryNotFoundException() : base("Library does not exist.") { }
}

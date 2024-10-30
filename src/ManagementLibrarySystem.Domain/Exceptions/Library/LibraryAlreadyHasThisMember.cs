using System;

namespace ManagementLibrarySystem.Domain.Exceptions.Library;

public class LibraryAlreadyHasThisMember : Exception
{
    public LibraryAlreadyHasThisMember() : base("Library already has this member registered.") { }
}

using System;

namespace ManagementLibrarySystem.Domain.Exceptions.Library;

public class DuplicateLibraryNameException : Exception
{
    public DuplicateLibraryNameException() : base("A library with the same name already exists.") { }

}

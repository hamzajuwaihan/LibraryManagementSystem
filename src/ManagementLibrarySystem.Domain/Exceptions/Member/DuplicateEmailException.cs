using System;

namespace ManagementLibrarySystem.Domain.Exceptions.Member;

public class DuplicateEmailException : Exception
{
    public DuplicateEmailException() : base("A member with the same email already exists.") { }
}

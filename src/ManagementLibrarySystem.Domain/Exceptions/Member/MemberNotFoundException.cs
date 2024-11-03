namespace ManagementLibrarySystem.Domain.Exceptions.Member;

public class MemberNotFoundException : Exception
{
    public MemberNotFoundException() : base("The specified member was not found.")
    {

    }

}

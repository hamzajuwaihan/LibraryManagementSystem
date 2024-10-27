using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;

public class Member : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<Book> Books { get; private set; } = [];
    public List<Library> Libraries { get; private set; } = [];

    public Member(Guid id) : base(Guid.Empty) { }

    public Member(Guid id, string name, string email) : base(id)
    {
        Name = name;
        Email = email;
    }

    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

}

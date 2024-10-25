using System;
using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;

public class Library : Entity
{
    public List<Book> Books { get; set; } = [];
    public List<Member> Members { get; set; } = [];
    public required string Name { get; set; }
    public Library(Guid id) : base(Guid.Empty) { }

    public Library(Guid id, string name) : base(id)
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
    }
}

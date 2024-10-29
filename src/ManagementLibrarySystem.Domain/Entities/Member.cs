using System.Text.Json.Serialization;
using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;
/// <summary>
/// Member Domain
/// </summary>
public class Member : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public List<Book> Books { get; private set; } = [];
    public List<Library> Libraries { get; private set; } = [];

    public Member(Guid id) : base(id) { }
    /// <summary>
    /// Constructor to create a new member, with JSON constructor attribute for unit testing
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="email"></param>
    [JsonConstructor]
    public Member(Guid id, string name, string email) : base(id)
    {
        Name = name;
        Email = email;
    }
    /// <summary>
    /// update the name and email for the member
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

}

using System.Text.Json.Serialization;
using ManagementLibrarySystem.Domain.Primitives;

namespace ManagementLibrarySystem.Domain.Entities;

public class Library : Entity
{
    public List<Book> Books { get; set; } = [];
    public List<Member> Members { get; set; } = [];
    public required string Name { get; set; }
    /// <summary>
    /// Cosntructor for EF Core
    /// </summary>
    /// <param name="id"></param>
    public Library(Guid id) : base(id) { }
    /// <summary>
    /// general constrcutor with JsonConstructor for unit testing (Attribute)
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    [JsonConstructor]
    public Library(Guid id, string name) : base(id)
    {
        Name = name;
    }
    /// <summary>
    /// updates the name of the library
    /// </summary>
    /// <param name="name">new name</param>
    public void Update(string name)
    {
        Name = name;
    }
}

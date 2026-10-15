namespace EduHub.Ch01.Domain;

/// <summary>Represents a student club.</summary>
public sealed class Club
{
    /// <summary>Gets the club identifier.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the club name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the club description.</summary>
    public string Description { get; private set; }

    /// <summary>Gets the date the club was created.</summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>Gets the list of club members.</summary>
    public List<Member> Members { get; private set; }

    private Club(Guid id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        CreatedAt = DateTime.Now;
        Members = new List<Member>();
    }

    /// <summary>Creates a club after validating its data.</summary>
    public static Club Create(string name, string description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Club name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        return new Club(Guid.NewGuid(), name.Trim(), description.Trim());
    }

    /// <summary>Adds a member to the club if not already present.</summary>
    public void AddMember(Member member)
    {
        ArgumentNullException.ThrowIfNull(member);

        if (Members.Exists(m => m.Id == member.Id))
            throw new InvalidOperationException("Member already exists in this club.");

        Members.Add(member);
    }

    /// <summary>Returns the number of club members.</summary>
    public int MemberCount()
    {
        return Members.Count;
    }
}

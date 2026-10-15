namespace EduHub.Ch01.Domain;

/// <summary>Represents a student who participates in club activities.</summary>
public sealed class Member
{
    /// <summary>Gets the member identifier.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the member name.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the member email.</summary>
    public string Email { get; private set; }

    /// <summary>Gets the student code.</summary>
    public string StudentCode { get; private set; }

    /// <summary>Gets the date the member joined.</summary>
    public DateTime JoinedAt { get; private set; }

    private Member(Guid id, string name, string email, string studentCode, DateTime joinedAt)
    {
        Id = id;
        Name = name;
        Email = email;
        StudentCode = studentCode;
        JoinedAt = joinedAt;
    }

    /// <summary>Creates a member after validating its data.</summary>
    public static Member Create(string name, string email, string studentCode)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            throw new ArgumentException("A valid email is required.", nameof(email));

        if (string.IsNullOrWhiteSpace(studentCode))
            throw new ArgumentException("Student code is required.", nameof(studentCode));

        return new Member(Guid.NewGuid(), name.Trim(), email.Trim(), studentCode.Trim(), DateTime.Now);
    }

    /// <summary>Returns true if the member has valid name, email and student code.</summary>
    public bool IsActive()
    {
        return !string.IsNullOrWhiteSpace(Name)
            && !string.IsNullOrWhiteSpace(Email)
            && !string.IsNullOrWhiteSpace(StudentCode);
    }
}

namespace EduHub.Ch01.Domain;

/// <summary>Represents an event organized by a club.</summary>
public sealed class Event
{
    /// <summary>Gets the event identifier.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the owning club identifier.</summary>
    public Guid ClubId { get; private set; }

    /// <summary>Gets the event title.</summary>
    public string Title { get; private set; }

    /// <summary>Gets the event date and time.</summary>
    public DateTime Date { get; private set; }

    /// <summary>Gets the maximum capacity.</summary>
    public int Capacity { get; private set; }

    /// <summary>Indicates whether the event is held online.</summary>
    public bool IsOnline { get; private set; }

    private Event(Guid id, Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        Id = id;
        ClubId = clubId;
        Title = title;
        Date = date;
        Capacity = capacity;
        IsOnline = isOnline;
    }

    /// <summary>Creates an event after validating its data.</summary>
    public static Event Create(Guid clubId, string title, DateTime date, int capacity, bool isOnline)
    {
        if (clubId == Guid.Empty)
            throw new ArgumentException("Club ID is required.", nameof(clubId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required.", nameof(title));

        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");

        return new Event(Guid.NewGuid(), clubId, title.Trim(), date, capacity, isOnline);
    }

    /// <summary>Returns true if the event still has available spots.</summary>
    public bool HasCapacity(int registeredCount)
    {
        if (registeredCount < 0)
            throw new ArgumentOutOfRangeException(nameof(registeredCount), "Registered count cannot be negative.");

        return registeredCount < Capacity;
    }
}

namespace EduHub.Ch01.Domain;

/// <summary>Defines the possible states of a registration.</summary>
public enum RegistrationStatus
{
    /// <summary>The registration is awaiting confirmation.</summary>
    Pending,

    /// <summary>The registration has been confirmed.</summary>
    Confirmed,

    /// <summary>The registration has been cancelled.</summary>
    Cancelled
}

/// <summary>Represents a member's registration for an event.</summary>
public sealed class Registration
{
    /// <summary>Gets the registration identifier.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the member identifier.</summary>
    public Guid MemberId { get; private set; }

    /// <summary>Gets the event identifier.</summary>
    public Guid EventId { get; private set; }

    /// <summary>Gets the date and time the registration was made.</summary>
    public DateTime RegisteredAt { get; private set; }

    /// <summary>Gets the current registration status.</summary>
    public RegistrationStatus Status { get; private set; }

    private Registration(Guid id, Guid memberId, Guid eventId)
    {
        Id = id;
        MemberId = memberId;
        EventId = eventId;
        RegisteredAt = DateTime.Now;
        Status = RegistrationStatus.Pending;
    }

    /// <summary>Creates a new pending registration.</summary>
    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("Member ID is required.", nameof(memberId));

        if (eventId == Guid.Empty)
            throw new ArgumentException("Event ID is required.", nameof(eventId));

        return new Registration(Guid.NewGuid(), memberId, eventId);
    }

    /// <summary>Confirms a pending registration.</summary>
    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException(
                $"Cannot confirm a '{Status}' registration. Only 'Pending' registrations can be confirmed.");

        Status = RegistrationStatus.Confirmed;
    }

    /// <summary>Cancels a pending or confirmed registration.</summary>
    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Registration is already cancelled.");

        Status = RegistrationStatus.Cancelled;
    }
}

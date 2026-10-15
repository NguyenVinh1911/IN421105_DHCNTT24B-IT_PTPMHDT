namespace EduHub.Ch01.Domain;

/// <summary>Defines the supported check-in methods.</summary>
public enum CheckInMethod
{
    /// <summary>Check-in using a QR code scan.</summary>
    QRCode,

    /// <summary>Check-in recorded manually by an organizer.</summary>
    Manual
}

/// <summary>Records attendance for a confirmed registration.</summary>
public sealed class CheckIn
{
    /// <summary>Gets the check-in identifier.</summary>
    public Guid Id { get; private set; }

    /// <summary>Gets the related registration identifier.</summary>
    public Guid RegistrationId { get; private set; }

    /// <summary>Gets the exact date and time of check-in.</summary>
    public DateTime CheckedInAt { get; private set; }

    /// <summary>Gets the method used for check-in.</summary>
    public CheckInMethod Method { get; private set; }

    private CheckIn(Guid id, Guid registrationId, CheckInMethod method)
    {
        Id = id;
        RegistrationId = registrationId;
        Method = method;
        CheckedInAt = DateTime.Now;
    }

    /// <summary>Creates a check-in record for the specified registration.</summary>
    public static CheckIn Create(Guid registrationId, CheckInMethod method)
    {
        if (registrationId == Guid.Empty)
            throw new ArgumentException("Registration ID is required.", nameof(registrationId));

        if (!Enum.IsDefined(method))
            throw new ArgumentOutOfRangeException(nameof(method), "Invalid check-in method.");

        return new CheckIn(Guid.NewGuid(), registrationId, method);
    }

    /// <summary>Returns a human-readable summary of this check-in record.</summary>
    public string Summary()
    {
        return $"CheckIn | RegistrationId: {RegistrationId} | Time: {CheckedInAt:dd/MM/yyyy HH:mm:ss} | Method: {Method}";
    }
}

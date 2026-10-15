```mermaid 

classDiagram
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +bool IsActive()
    }

    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +List~Member~ Members
        +void AddMember(Member member)
        +int MemberCount()
    }

    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +bool HasCapacity(int registeredCount)
    }

    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +RegistrationStatus Status
        +void Confirm()
        +void Cancel()
    }

    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +CheckInMethod Method
        +string Summary()
    }

    Club "1" --> "*" Member : manages
    Club "1" --> "*" Event : organizes
    Member "1" --> "*" Registration : registers
    Event "1" --> "*" Registration : has
    Registration "1" --> "0..1" CheckIn : records

using EduHub.Ch01.Domain;

const int EventCapacity = 50;

try
{
    // ============================
    // Step 1: Create a club
    // ============================
    Console.WriteLine("==== STEP 1: CREATE CLUB ====");

    Club club = Club.Create(
        "Tech Club",
        "A club for students interested in technology and AI.");

    Console.WriteLine($"Club: {club.Name}");
    Console.WriteLine($"Description: {club.Description}");

    // ============================
    // Step 2: Add three members
    // ============================
    Console.WriteLine("\n==== STEP 2: ADD MEMBERS ====");

    Member member1 = Member.Create("Nguyen An",  "an@example.com",   "SV001");
    Member member2 = Member.Create("Tran Binh",  "binh@example.com", "SV002");
    Member member3 = Member.Create("Le Chi",     "chi@example.com",  "SV003");

    club.AddMember(member1);
    club.AddMember(member2);
    club.AddMember(member3);

    Console.WriteLine($"Member count: {club.MemberCount()}");

    foreach (Member member in club.Members)
    {
        Console.WriteLine($"  {member.StudentCode} - {member.Name} - {member.Email}");
    }

    // ============================
    // Step 3: Create an event
    // ============================
    Console.WriteLine("\n==== STEP 3: CREATE EVENT ====");

    Event workshop = Event.Create(
        club.Id,
        "Workshop AI",
        DateTime.Now.AddDays(7),
        EventCapacity,
        isOnline: false);

    Console.WriteLine($"Event: {workshop.Title}");
    Console.WriteLine($"Capacity: {workshop.Capacity}");
    Console.WriteLine($"Date: {workshop.Date:dd/MM/yyyy HH:mm}");

    // ============================
    // Step 4: Register 2 members
    // ============================
    Console.WriteLine("\n==== STEP 4: REGISTER MEMBERS ====");

    List<Registration> registrations = new List<Registration>();

    // Register member1
    if (workshop.HasCapacity(registrations.Count))
    {
        Registration registration1 = Registration.Create(member1.Id, workshop.Id);
        registrations.Add(registration1);
        Console.WriteLine($"{member1.Name}: {registration1.Status}");

        registration1.Confirm();
        Console.WriteLine($"{member1.Name}: {registration1.Status}");
    }

    // Register member2
    if (workshop.HasCapacity(registrations.Count))
    {
        Registration registration2 = Registration.Create(member2.Id, workshop.Id);
        registrations.Add(registration2);
        Console.WriteLine($"{member2.Name}: {registration2.Status}");

        registration2.Confirm();
        Console.WriteLine($"{member2.Name}: {registration2.Status}");
    }

    // ============================
    // Step 5: Check-in
    // ============================
    Console.WriteLine("\n==== STEP 5: CHECK-IN ====");

    List<CheckIn> checkIns = new List<CheckIn>();

    Registration firstRegistration = registrations[0];

    if (firstRegistration.Status == RegistrationStatus.Confirmed)
    {
        CheckIn checkIn = CheckIn.Create(firstRegistration.Id, CheckInMethod.QRCode);
        checkIns.Add(checkIn);
        Console.WriteLine(checkIn.Summary());
    }

    // ============================
    // Step 6: Attendance report
    // ============================
    Console.WriteLine("\n==== STEP 6: ATTENDANCE REPORT ====");

    Console.WriteLine($"Club: {club.Name}");
    Console.WriteLine($"Event: {workshop.Title}");
    Console.WriteLine($"Total registrations: {registrations.Count}");
    Console.WriteLine($"Total check-ins: {checkIns.Count}");

    Console.WriteLine("Registered members:");

    foreach (Registration reg in registrations)
    {
        Member? registeredMember = club.Members.Find(m => m.Id == reg.MemberId);

        if (registeredMember != null)
        {
            Console.WriteLine($"  - {registeredMember.Name}: {reg.Status}");
        }
    }

    // ============================
    // Step 7: Test business rule
    // ============================
    Console.WriteLine("\n==== STEP 7: TEST BUSINESS RULE ====");

    Registration cancelledRegistration = Registration.Create(member3.Id, workshop.Id);
    cancelledRegistration.Cancel();

    try
    {
        cancelledRegistration.Confirm(); // Must throw InvalidOperationException
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Expected error: {ex.Message}");
    }

    Console.WriteLine("\n==== DEMO COMPLETED ====");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Invalid input: {ex.Message}");
}

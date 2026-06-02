namespace Assignment01.Models;

using Assignment01.Enums;

public class Bus
{
    private static readonly Dictionary<CoachVariant, int> SeatingCapacity = new()
    {
        { CoachVariant.Economy, 50 },
        { CoachVariant.Business, 30 },
    };

    public string BusId { get; private set; }
    public string CoachNumber { get; private set; }
    public CoachVariant CoachClass { get; private set; }
    public int TotalSeats { get; private set; }

    public Bus(string busId, string coachNumber, CoachVariant coachVariant)
    {
        Validate(coachNumber);

        BusId = busId;
        CoachNumber = coachNumber;
        CoachClass = coachVariant;
        TotalSeats = GetSeatCapacity(coachVariant);
    }

    // Lets a caller check the inputs before generating an ID, so a rejected bus wastes no ID.
    public static void Validate(string coachNumber)
    {
        if (string.IsNullOrWhiteSpace(coachNumber))
            throw new ArgumentException("Coach number is required.");
    }

    public static int GetSeatCapacity(CoachVariant coachVariant) =>
        SeatingCapacity.TryGetValue(coachVariant, out int seats)
            ? seats
            : throw new ArgumentException($"No seating capacity defined for {coachVariant}.");

    public override string ToString()
    {
        return $"[{BusId}] Coach: {CoachNumber} | Class: {CoachClass} | Seats: {TotalSeats}";
    }
}

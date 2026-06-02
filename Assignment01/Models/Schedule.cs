namespace Assignment01.Models;

public class Schedule
{
    public string ScheduleId { get; private set; }
    public string DepartureCity { get; private set; }
    public string ArrivalCity { get; private set; }
    public DateTime DepartureDateTime { get; private set; }
    public decimal TicketPrice { get; private set; }
    public Bus BusRef { get; private set; }

    // seat numbers already booked on this trip
    private readonly HashSet<int> _reservedSeat = new();

    public Schedule(
        string scheduleId,
        string departCity,
        string arrivalCity,
        DateTime departDateTime,
        decimal ticketPrice,
        Bus bus
    )
    {
        Validate(departCity, arrivalCity, departDateTime, ticketPrice);

        ScheduleId = scheduleId;
        DepartureCity = departCity;
        ArrivalCity = arrivalCity;
        DepartureDateTime = departDateTime;
        TicketPrice = ticketPrice;
        BusRef = bus;
    }

    // Lets a caller check the inputs before generating an ID, so a rejected schedule wastes no ID.
    public static void Validate(
        string departCity,
        string arrivalCity,
        DateTime departDateTime,
        decimal ticketPrice
    )
    {
        if (string.IsNullOrWhiteSpace(departCity))
            throw new ArgumentException("Departure city is required.");
        if (string.IsNullOrWhiteSpace(arrivalCity))
            throw new ArgumentException("Arrival city is required.");
        if (ticketPrice <= 0)
            throw new ArgumentException("Ticket price must be greater than zero.");
        if (departDateTime <= DateTime.Now)
            throw new ArgumentException("Departure must be in the future.");
    }

    public void ReserveSeat(int seatNumber)
    {
        // guard the invariant here too, not just at the call site
        if (!IsSeatAvailable(seatNumber))
            throw new InvalidOperationException($"Seat {seatNumber} is not available.");

        _reservedSeat.Add(seatNumber);
    }

    public bool IsSeatAvailable(int seatNumber)
    {
        return seatNumber >= 1
            && seatNumber <= BusRef.TotalSeats
            && !_reservedSeat.Contains(seatNumber);
    }

    // return a copy so callers can't mutate our internal set
    public IReadOnlyCollection<int> GetReservedSeats() => _reservedSeat.ToArray();

    public int AvailableSeats => BusRef.TotalSeats - _reservedSeat.Count;

    public override string ToString()
    {
        return $"[{ScheduleId}] {DepartureCity} -> {ArrivalCity} | "
            + $"Departure: {DepartureDateTime:yyyy-MM-dd HH:mm} | "
            + $"Price: {TicketPrice:C} | Bus: {BusRef.BusId} ({BusRef.CoachClass}) | "
            + $"Available Seats: {AvailableSeats}/{BusRef.TotalSeats}";
    }
}

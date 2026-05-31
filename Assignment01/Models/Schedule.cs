namespace Assignment01.Models;

public class Schedule
{
    public string ScheduleId { get; private set; }
    public string DepartureCity { get; private set; }
    public string ArrivalCity { get; private set; }
    public DateTime DepartureDateTime { get; private set; }
    public decimal TicketPrice { get; private set; }
    public Bus BusRef { get; private set; }
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
        ScheduleId = scheduleId;
        DepartureCity = departCity;
        ArrivalCity = arrivalCity;
        DepartureDateTime = departDateTime;
        TicketPrice = ticketPrice;
        BusRef = bus;
    }

    public void ReserveSeat(int seatNumber) => _reservedSeat.Add(seatNumber);

    public bool IsSeatAvailable(int seatNumber)
    {
        return seatNumber >= 1
            && seatNumber <= BusRef.TotalSeats
            && !_reservedSeat.Contains(seatNumber);
    }

    public IReadOnlyCollection<int> GetReservedSeats() => _reservedSeat;

    public int AvailableSeats => BusRef.TotalSeats - _reservedSeat.Count;

    public override string ToString()
    {
        return $"[{ScheduleId}] {DepartureCity} -> {ArrivalCity} | "
            + $"Departure: {DepartureDateTime:yyyy-MM-dd HH:mm} | "
            + $"Price: {TicketPrice:C} | Bus: {BusRef.BusId} ({BusRef.CoachClass}) | "
            + $"Available Seats: {AvailableSeats}/{BusRef.TotalSeats}";
    }
}

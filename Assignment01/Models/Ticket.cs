namespace Assignment01.Models;

public class Ticket
{
    public string TicketId { get; private set; }
    public User UserRef { get; private set; }
    public Schedule ScheduleRef { get; private set; }
    public int SeatNumber { get; private set; }
    public DateTime BookingDate { get; private set; }

    public Ticket(string ticketId, User user, Schedule schedule, int seatNumber)
    {
        TicketId = ticketId;
        UserRef = user;
        ScheduleRef = schedule;
        SeatNumber = seatNumber;
        BookingDate = DateTime.UtcNow;
    }

    public override string ToString()
    {
        return $"[{TicketId}] User: {UserRef?.FullName} | "
            + $"Route: {ScheduleRef?.DepartureCity} -> {ScheduleRef?.ArrivalCity} | "
            + $"Departure: {ScheduleRef?.DepartureDateTime:yyyy-MM-dd HH:mm} | "
            + $"Seat: {SeatNumber} ({ScheduleRef?.BusRef?.CoachClass}) | "
            + $"Booked: {BookingDate:yyyy-MM-dd HH:mm}";
    }
}

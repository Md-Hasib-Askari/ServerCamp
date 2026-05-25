namespace Assignment01.Interfaces.Services;

using Assignment01.Models;

public interface IBookingService
{
    (Ticket ticket, Invoice invoice) BookTicket(string userId, string scheduleId, int seatNumber);
    IEnumerable<Ticket> GetUserTickets(string userId);
}

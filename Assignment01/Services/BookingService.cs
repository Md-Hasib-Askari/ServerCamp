using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

namespace Assignment01.Services;

public class BookingService : IBookingService
{
    private readonly IBaseRepository<User> _userRepo;
    private readonly IBaseRepository<Schedule> _scheduleRepo;
    private readonly IBaseRepository<Ticket> _ticketRepo;
    private readonly IBaseRepository<Invoice> _invoiceRepo;
    private readonly IIdGenerator _idGenerator;

    public BookingService(
        IBaseRepository<User> userRepo,
        IBaseRepository<Schedule> scheduleRepo,
        IBaseRepository<Ticket> ticketRepo,
        IBaseRepository<Invoice> invoiceRepo,
        IIdGenerator idGenerator
    )
    {
        _userRepo = userRepo;
        _scheduleRepo = scheduleRepo;
        _ticketRepo = ticketRepo;
        _invoiceRepo = invoiceRepo;
        _idGenerator = idGenerator;
    }

    public (Ticket ticket, Invoice invoice) BookTicket(
        string userId,
        string scheduleId,
        int seatNumber
    )
    {
        var user =
            _userRepo.GetById(userId)
            ?? throw new KeyNotFoundException($"User '{userId}' not found.");

        var schedule =
            _scheduleRepo.GetById(scheduleId)
            ?? throw new KeyNotFoundException($"Schedule '{scheduleId}' not found.");

        // Block only seats already confirmed by a payment - unpaid bookings don't hold the seat
        if (!schedule.IsSeatAvailable(seatNumber))
            throw new InvalidOperationException($"Seat {seatNumber} is not available.");

        var ticket = new Ticket(_idGenerator.GenerateTicketId(), user, schedule, seatNumber);
        _ticketRepo.Add(ticket);

        var invoice = new Invoice(_idGenerator.GenerateInvoiceId(), ticket, schedule.TicketPrice);
        _invoiceRepo.Add(invoice);

        return (ticket, invoice);
    }

    public IEnumerable<Ticket> GetUserTickets(string userId)
    {
        // Validate the user exists
        if (_userRepo.GetById(userId) is null)
            throw new KeyNotFoundException($"User '{userId}' not found.");

        return _ticketRepo.GetAll().Where(t => t.UserRef.UserId == userId);
    }
}

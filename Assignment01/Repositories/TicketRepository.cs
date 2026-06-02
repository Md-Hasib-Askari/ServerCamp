using Assignment01.Interfaces;
using Assignment01.Models;

namespace Assignment01.Services;

public class TicketRepository : IBaseRepository<Ticket>
{
    private readonly Dictionary<string, Ticket> _tickets = new();

    public void Add(Ticket ticket) => _tickets[ticket.TicketId] = ticket;

    public bool Exists(string ticketId) => _tickets.ContainsKey(ticketId);

    public IEnumerable<Ticket> GetAll() => _tickets.Values;

    public Ticket? GetById(string ticketId) => _tickets.TryGetValue(ticketId, out var t) ? t : null;
}

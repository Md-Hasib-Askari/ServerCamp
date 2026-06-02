using Assignment01.Interfaces;
using Assignment01.Models;

namespace Assignment01.Services;

public class InvoiceRepository : IBaseRepository<Invoice>
{
    private readonly Dictionary<string, Invoice> _invoices = new();

    public void Add(Invoice invoice) => _invoices[invoice.InvoiceId] = invoice;

    public IEnumerable<Invoice> GetAll() => _invoices.Values;

    public Invoice? GetById(string invoiceId) =>
        _invoices.TryGetValue(invoiceId, out var i) ? i : null;
}

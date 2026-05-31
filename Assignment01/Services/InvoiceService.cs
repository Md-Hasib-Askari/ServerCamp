using Assignment01.Enums;
using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

namespace Assignment01.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IBaseRepository<Invoice> _invoiceRepo;
    private readonly IBaseRepository<User> _userRepo;

    public InvoiceService(IBaseRepository<Invoice> invoiceRepo, IBaseRepository<User> userRepo)
    {
        _invoiceRepo = invoiceRepo;
        _userRepo = userRepo;
    }

    public IEnumerable<Invoice> GetUserInvoices(string userId) =>
        _invoiceRepo.GetAll().Where(i => i.UserId == userId);

    public Invoice ProcessPayment(string invoiceId)
    {
        var invoice =
            _invoiceRepo.GetById(invoiceId)
            ?? throw new KeyNotFoundException($"Invoice '{invoiceId}' not found.");

        if (invoice.Status == PaymentStatus.Paid)
            throw new InvalidOperationException($"Invoice '{invoiceId}' is already paid.");

        invoice.MarkAsPaid();
        return invoice;
    }
}

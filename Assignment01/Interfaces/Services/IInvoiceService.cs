namespace Assignment01.Interfaces.Services;

using Assignment01.Models;

public interface IInvoiceService
{
    IEnumerable<Invoice> GetUserInvoices(string userId);
    Invoice ProcessPayment(string invoiceId);
}

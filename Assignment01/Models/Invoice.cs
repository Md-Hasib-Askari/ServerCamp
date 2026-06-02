namespace Assignment01.Models;

using Assignment01.Enums;

public class Invoice
{
    public string InvoiceId { get; private set; }
    public Ticket TicketRef { get; private set; }

    // pulled from the ticket's user so we don't store it twice
    public string UserId => TicketRef.UserRef.UserId;
    public decimal AmountDue { get; private set; }
    public DateTime GenerationDate { get; private set; }
    public PaymentStatus Status { get; private set; }

    public Invoice(string invoiceId, Ticket ticket, decimal amountDue)
    {
        InvoiceId = invoiceId;
        TicketRef = ticket;
        AmountDue = amountDue;
        GenerationDate = DateTime.UtcNow;
        Status = PaymentStatus.Unpaid;
    }

    public void MarkAsPaid()
    {
        Status = PaymentStatus.Paid;
    }

    public override string ToString()
    {
        return $"[{InvoiceId}] Ticket: {TicketRef.TicketId} | User: {UserId} | "
            + $"Amount: {AmountDue:C} | Date: {GenerationDate:yyyy-MM-dd HH:mm} | "
            + $"Status: {Status}";
    }
}

namespace Assignment01.Interfaces.Services;

public interface IIdGenerator
{
    string GenerateUserId();
    string GenerateBusId();
    string GenerateScheduleId();
    string GenerateTicketId();
    string GenerateInvoiceId();
}

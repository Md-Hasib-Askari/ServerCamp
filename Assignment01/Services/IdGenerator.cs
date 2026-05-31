using Assignment01.Interfaces.Services;

namespace Assignment01.Services;

public class IdGenerator : IIdGenerator
{
    private int _userCounter = 0;
    private int _busCounter = 0;
    private int _scheduleCounter = 0;
    private int _ticketCounter = 0;
    private int _invoiceCounter = 0;

    public string GenerateUserId() => $"USR-{++_userCounter}";

    public string GenerateBusId() => $"BUS-{++_busCounter}";

    public string GenerateScheduleId() => $"SCH-{++_scheduleCounter}";

    public string GenerateTicketId() => $"TKT-{++_ticketCounter}";

    public string GenerateInvoiceId() => $"INV-{++_invoiceCounter}";
}

namespace Assignment01.UI;

using Assignment01.Enums;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

public class MenuHandler
{
    private readonly IUserService _userService;
    private readonly IBusService _busService;
    private readonly IScheduleService _scheduleService;
    private readonly IBookingService _bookingService;
    private readonly IInvoiceService _invoiceService;

    public MenuHandler(
        IUserService userService,
        IBusService busService,
        IScheduleService scheduleService,
        IBookingService bookingService,
        IInvoiceService invoiceService
    )
    {
        _userService = userService;
        _busService = busService;
        _scheduleService = scheduleService;
        _bookingService = bookingService;
        _invoiceService = invoiceService;
    }

    //  User Operations

    public void HandleCreateUser()
    {
        ConsoleHelper.PrintHeader("Create New User");
        try
        {
            string name = ConsoleHelper.ReadInput("Full Name");
            string mobile = ConsoleHelper.ReadInput("Mobile Number");
            string email = ConsoleHelper.ReadInput("Email Address");

            var user = _userService.CreateUser(name, mobile, email);
            ConsoleHelper.PrintSuccess($"User created successfully! ID: {user.UserId}");
            ConsoleHelper.PrintItem(user.ToString());
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    public void HandleDisplayAllUsers()
    {
        ConsoleHelper.PrintHeader("All Users");
        var users = _userService.GetAllUsers().ToList();
        if (!users.Any())
        {
            ConsoleHelper.PrintInfo("No users registered yet.");
        }
        else
        {
            users.ForEach(u => ConsoleHelper.PrintItem(u.ToString()));
        }
        ConsoleHelper.PressAnyKey();
    }

    //  Bus Operations

    public void HandleCreateBus()
    {
        ConsoleHelper.PrintHeader("Create New Bus");
        try
        {
            string coach = ConsoleHelper.ReadInput("Coach Number");
            Console.WriteLine();
            Console.WriteLine("  Bus Coach Class:");
            Console.WriteLine($"    1. Economy  ({Bus.GetSeatCapacity(CoachVariant.Economy)} seats)");
            Console.WriteLine($"    2. Business ({Bus.GetSeatCapacity(CoachVariant.Business)} seats)");
            string classChoice = ConsoleHelper.ReadInput("Select Coach Class (1/2)");

            CoachVariant coachClass = classChoice switch
            {
                "1" => CoachVariant.Economy,
                "2" => CoachVariant.Business,
                _ => throw new ArgumentException("Invalid coachClass. Enter 1 or 2."),
            };

            var bus = _busService.CreateBus(coach, coachClass);
            ConsoleHelper.PrintSuccess($"Bus created successfully! ID: {bus.BusId}");
            ConsoleHelper.PrintItem(bus.ToString());
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    public void HandleDisplayAllBuses()
    {
        ConsoleHelper.PrintHeader("All Buses");
        var buses = _busService.GetAllBuses().ToList();
        if (!buses.Any())
        {
            ConsoleHelper.PrintInfo("No buses registered yet.");
        }
        else
        {
            buses.ForEach(b => ConsoleHelper.PrintItem(b.ToString()));
        }
        ConsoleHelper.PressAnyKey();
    }

    //  Schedule Operations

    public void HandleCreateSchedule()
    {
        ConsoleHelper.PrintHeader("Create New Schedule");
        try
        {
            var buses = _busService.GetAllBuses().ToList();
            if (!buses.Any())
            {
                ConsoleHelper.PrintInfo("No buses available. Create a bus first.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Available Buses:");
            buses.ForEach(b => ConsoleHelper.PrintItem(b.ToString()));
            ConsoleHelper.PrintSeparator();

            string dep = ConsoleHelper.ReadInput("Departure City");
            string arr = ConsoleHelper.ReadInput("Arrival City");
            string dateStr = ConsoleHelper.ReadInput("Departure Date & Time (yyyy-MM-dd HH:mm)");
            string priceStr = ConsoleHelper.ReadInput("Ticket Price (BDT)");
            string busId = ConsoleHelper.ReadInput("Bus ID");

            if (
                !DateTime.TryParseExact(
                    dateStr,
                    "yyyy-MM-dd HH:mm",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime depDateTime
                )
            )
                throw new FormatException("Invalid date format. Use: yyyy-MM-dd HH:mm");

            if (!decimal.TryParse(priceStr, out decimal price))
                throw new FormatException("Invalid price. Enter a numeric value.");

            var schedule = _scheduleService.CreateSchedule(dep, arr, depDateTime, price, busId);
            ConsoleHelper.PrintSuccess($"Schedule created successfully! ID: {schedule.ScheduleId}");
            ConsoleHelper.PrintItem(schedule.ToString());
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    public void HandleDisplayAllSchedules()
    {
        ConsoleHelper.PrintHeader("All Schedules");
        var schedules = _scheduleService.GetAllSchedule().ToList();
        if (!schedules.Any())
        {
            ConsoleHelper.PrintInfo("No schedules created yet.");
        }
        else
        {
            schedules.ForEach(s => ConsoleHelper.PrintItem(s.ToString()));
        }
        ConsoleHelper.PressAnyKey();
    }

    public void HandleDisplayScheduleDetails()
    {
        ConsoleHelper.PrintHeader("Schedule Details");
        try
        {
            var schedules = _scheduleService.GetAllSchedule().ToList();
            if (!schedules.Any())
            {
                ConsoleHelper.PrintInfo("No schedules created yet.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Available Schedules:");
            schedules.ForEach(s => ConsoleHelper.PrintItem(s.ToString()));
            ConsoleHelper.PrintSeparator();

            string id = ConsoleHelper.ReadInput("Schedule ID");
            var schedule =
                _scheduleService.GetScheduleById(id)
                ?? throw new KeyNotFoundException($"Schedule '{id}' not found.");

            ConsoleHelper.PrintSeparator();
            ConsoleHelper.PrintItem($"Schedule ID   : {schedule.ScheduleId}");
            ConsoleHelper.PrintItem(
                $"Route         : {schedule.DepartureCity} → {schedule.ArrivalCity}"
            );
            ConsoleHelper.PrintItem(
                $"Departure     : {schedule.DepartureDateTime:yyyy-MM-dd HH:mm}"
            );
            ConsoleHelper.PrintItem($"Ticket Price  : {schedule.TicketPrice:C}");
            ConsoleHelper.PrintItem($"Bus ID        : {schedule.BusRef.BusId}");
            ConsoleHelper.PrintItem($"Bus Class     : {schedule.BusRef.CoachClass}");
            ConsoleHelper.PrintItem($"Total Seats   : {schedule.BusRef.TotalSeats}");
            ConsoleHelper.PrintItem($"Available     : {schedule.AvailableSeats}");
            PrintSeatGrid(schedule);
            ConsoleHelper.PrintSeparator();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    //  Booking Operations

    public void HandleBookTicket()
    {
        ConsoleHelper.PrintHeader("Book a Ticket");
        try
        {
            // Show schedules for reference
            var schedules = _scheduleService.GetAllSchedule().ToList();
            if (!schedules.Any())
            {
                ConsoleHelper.PrintInfo("No schedules available to book.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            var users = _userService.GetAllUsers().ToList();
            if (!users.Any())
            {
                ConsoleHelper.PrintInfo("No users registered. Create a user first.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Registered Users:");
            users.ForEach(u => ConsoleHelper.PrintItem(u.ToString()));
            ConsoleHelper.PrintSeparator();

            ConsoleHelper.PrintInfo("Available Schedules:");
            schedules.ForEach(s => ConsoleHelper.PrintItem(s.ToString()));
            ConsoleHelper.PrintSeparator();

            string userId = ConsoleHelper.ReadInput("User ID");
            string scheduleId = ConsoleHelper.ReadInput("Schedule ID");

            var selectedSchedule =
                _scheduleService.GetScheduleById(scheduleId)
                ?? throw new KeyNotFoundException($"Schedule '{scheduleId}' not found.");

            PrintSeatGrid(selectedSchedule);

            string seatLabel = ConsoleHelper.ReadInput("Seat (e.g. 1A, 2B)");
            int seatNumber = ParseSeatLabel(seatLabel, selectedSchedule);

            var (ticket, invoice) = _bookingService.BookTicket(userId, scheduleId, seatNumber);

            ConsoleHelper.PrintSuccess("Ticket booked and invoice generated successfully!");
            ConsoleHelper.PrintSeparator();
            ConsoleHelper.PrintItem($"Ticket  : {ticket}");
            ConsoleHelper.PrintItem($"Invoice : {invoice}");
            ConsoleHelper.PrintSeparator();
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    public void HandleDisplayUserTickets()
    {
        ConsoleHelper.PrintHeader("User Tickets");
        try
        {
            var users = _userService.GetAllUsers().ToList();
            if (!users.Any())
            {
                ConsoleHelper.PrintInfo("No users registered yet.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Registered Users:");
            users.ForEach(u => ConsoleHelper.PrintItem(u.ToString()));
            ConsoleHelper.PrintSeparator();

            string userId = ConsoleHelper.ReadInput("User ID");
            var tickets = _bookingService.GetUserTickets(userId).ToList();
            if (!tickets.Any())
            {
                ConsoleHelper.PrintInfo("No tickets found for this user.");
            }
            else
            {
                tickets.ForEach(t => ConsoleHelper.PrintItem(t.ToString()));
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    //  Invoice Operations

    public void HandleDisplayUserInvoices()
    {
        ConsoleHelper.PrintHeader("User Invoices");
        try
        {
            var users = _userService.GetAllUsers().ToList();
            if (!users.Any())
            {
                ConsoleHelper.PrintInfo("No users registered yet.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Registered Users:");
            users.ForEach(u => ConsoleHelper.PrintItem(u.ToString()));
            ConsoleHelper.PrintSeparator();

            string userId = ConsoleHelper.ReadInput("User ID");
            var invoices = _invoiceService.GetUserInvoices(userId).ToList();
            if (!invoices.Any())
            {
                ConsoleHelper.PrintInfo("No invoices found for this user.");
            }
            else
            {
                invoices.ForEach(i => ConsoleHelper.PrintItem(i.ToString()));
            }
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }

    // "12A" -> seat number. Column count must match PrintSeatGrid.
    private static int ParseSeatLabel(string label, Schedule schedule)
    {
        label = label.Trim().ToUpper();
        if (label.Length < 2)
            throw new FormatException(
                "Invalid seat format. Use row + column letter (e.g. 1A, 2B)."
            );

        bool isBusiness = schedule.BusRef.CoachClass == CoachVariant.Business;
        int totalCols = isBusiness ? 3 : 4;

        // last char is the column, the rest is the row
        if (!int.TryParse(label[..^1], out int row) || row < 1)
            throw new FormatException(
                "Invalid row number. Must be a positive integer (e.g. 1A, 2C)."
            );

        // 'A' -> 0, 'B' -> 1, ...
        int colIndex = label[^1] - 'A';
        if (colIndex < 0 || colIndex >= totalCols)
        {
            string validCols = string.Join(
                ", ",
                Enumerable.Range(0, totalCols).Select(i => (char)('A' + i))
            );
            throw new FormatException($"Invalid column '{label[^1]}'. Valid columns: {validCols}.");
        }

        int seatNumber = (row - 1) * totalCols + colIndex + 1;
        if (seatNumber > schedule.BusRef.TotalSeats)
            throw new FormatException(
                $"Seat '{label}' does not exist. This bus has {schedule.BusRef.TotalSeats} seats."
            );

        return seatNumber;
    }

    private void PrintSeatGrid(Schedule schedule)
    {
        int totalSeats = schedule.BusRef.TotalSeats;
        var reserved = schedule.GetReservedSeats();
        bool isBusiness = schedule.BusRef.CoachClass == CoachVariant.Business;

        // Business: 1+2 (A | B C), Economy: 2+2 (A B | C D)
        int leftCols = isBusiness ? 1 : 2;
        int rightCols = isBusiness ? 2 : 2;
        int totalCols = leftCols + rightCols;
        int rows = (int)Math.Ceiling(totalSeats / (double)totalCols);

        Console.WriteLine();
        Console.Write("  Seat Map  ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[ ] Available  ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("[X] Booked");
        Console.ResetColor();
        Console.WriteLine("\n");

        // Column header
        Console.Write("      ");
        for (int c = 0; c < totalCols; c++)
        {
            if (c == leftCols)
                Console.Write("  |");
            Console.Write($"  {(char)('A' + c)} ");
        }
        Console.WriteLine();

        for (int r = 0; r < rows; r++)
        {
            Console.Write($"  {r + 1, 2}  ");
            for (int c = 0; c < totalCols; c++)
            {
                if (c == leftCols)
                    Console.Write("  |");

                int seatNum = r * totalCols + c + 1;
                if (seatNum > totalSeats)
                {
                    Console.Write("    ");
                    continue;
                }

                bool booked = reserved.Contains(seatNum);
                Console.Write(" ");
                Console.ForegroundColor = booked ? ConsoleColor.Red : ConsoleColor.Green;
                Console.Write(booked ? "[X]" : "[ ]");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void HandleProcessPayment()
    {
        ConsoleHelper.PrintHeader("Process Invoice Payment");
        try
        {
            var users = _userService.GetAllUsers().ToList();
            if (!users.Any())
            {
                ConsoleHelper.PrintInfo("No users registered yet.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("Registered Users:");
            users.ForEach(u => ConsoleHelper.PrintItem(u.ToString()));
            ConsoleHelper.PrintSeparator();

            string userId = ConsoleHelper.ReadInput("User ID");
            var invoices = _invoiceService.GetUserInvoices(userId).ToList();
            if (!invoices.Any())
            {
                ConsoleHelper.PrintInfo("No invoices found for this user.");
                ConsoleHelper.PressAnyKey();
                return;
            }

            ConsoleHelper.PrintInfo("User Invoices:");
            invoices.ForEach(i => ConsoleHelper.PrintItem(i.ToString()));
            ConsoleHelper.PrintSeparator();

            string invoiceId = ConsoleHelper.ReadInput("Invoice ID");
            var invoice = _invoiceService.ProcessPayment(invoiceId);
            ConsoleHelper.PrintSuccess(
                $"Payment processed successfully for Invoice {invoice.InvoiceId}."
            );
            ConsoleHelper.PrintItem(invoice.ToString());
        }
        catch (Exception ex)
        {
            ConsoleHelper.PrintError(ex.Message);
        }
        ConsoleHelper.PressAnyKey();
    }
}

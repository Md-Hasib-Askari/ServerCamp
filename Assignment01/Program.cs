using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;
using Assignment01.Repositories;
using Assignment01.Services;
using Assignment01.UI;

namespace Assignment01;

class Program
{
    static void Main(string[] args)
    {
        //  Dependency Injection (manual composition root)
        IIdGenerator idGenerator = new IdGenerator();

        // Repositories
        IBaseRepository<User> userRepo = new UserRepository();
        IBaseRepository<Bus> busRepo = new BusRepository();
        IBaseRepository<Schedule> scheduleRepo = new ScheduleRepository();
        IBaseRepository<Ticket> ticketRepo = new TicketRepository();
        IBaseRepository<Invoice> invoiceRepo = new InvoiceRepository();

        // Services
        IUserService userService = new UserService(userRepo, idGenerator);
        IBusService busService = new BusService(busRepo, idGenerator);
        IScheduleService scheduleService = new ScheduleService(scheduleRepo, busRepo, idGenerator);
        IBookingService bookingService = new BookingService(
            userRepo,
            scheduleRepo,
            ticketRepo,
            invoiceRepo,
            idGenerator
        );
        IInvoiceService invoiceService = new InvoiceService(invoiceRepo, userRepo);

        var menu = new MenuHandler(
            userService,
            busService,
            scheduleService,
            bookingService,
            invoiceService
        );

        //  Main Loop
        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                @"
  Bus Ticket Booking System
  Your journey starts right here!
  
  ==============================
  -- Manage Passengers --
  ==============================
    [1] Add a new passenger
    [2] View all passengers

  ==============================
  -- Manage Buses --
  ==============================
    [3] Register a new bus
    [4] View all buses

  ==============================
  -- Manage Schedules --
  ==============================
    [5] Add a new schedule
    [6] View all schedules
    [7] View schedule details
    [8] View available seats

  ==============================
  -- Book a Trip --
  ==============================
    [9] Book a ticket
   [10] View my tickets

  ==============================
  -- Billing and Payments --
  ==============================
   [11] View my invoices
   [12] Pay an invoice

  ==============================
    [0] Exit"
            );
            Console.ResetColor();

            Console.Write("\n  Select an option: ");
            string choice = Console.ReadLine()?.Trim() ?? "";
            Console.Clear();

            switch (choice)
            {
                case "1":
                    menu.HandleCreateUser();
                    break;
                case "2":
                    menu.HandleDisplayAllUsers();
                    break;
                case "3":
                    menu.HandleCreateBus();
                    break;
                case "4":
                    menu.HandleDisplayAllBuses();
                    break;
                case "5":
                    menu.HandleCreateSchedule();
                    break;
                case "6":
                    menu.HandleDisplayAllSchedules();
                    break;
                case "7":
                    menu.HandleDisplayScheduleDetails();
                    break;
                case "8":
                    menu.HandleDisplayAvailableSeats();
                    break;
                case "9":
                    menu.HandleBookTicket();
                    break;
                case "10":
                    menu.HandleDisplayUserTickets();
                    break;
                case "11":
                    menu.HandleDisplayUserInvoices();
                    break;
                case "12":
                    menu.HandleProcessPayment();
                    break;
                case "0":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(
                        "\n  Thank you for using Bus Ticket Booking System. Goodbye!\n"
                    );
                    Console.ResetColor();
                    running = false;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n  Invalid option. Please try again.");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}

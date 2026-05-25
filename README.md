# ServerCamp Assignment Repository

Coursework and assignment submissions for ServerCamp.

## Assignment01 - Bus Ticket Booking System

A .NET 10 console application that models a bus ticket booking workflow:
buses, schedules, users, ticket reservations, and invoice payments.

### Tech stack

- .NET 10 (`net10.0`)
- C# with nullable reference types and implicit usings enabled
- Console application (`OutputType=Exe`)

### Project layout

```
Assignment01/
├── Assignment01.csproj
├── Program.cs                 # Thread-safe singleton AppLogger
├── Enums/
│   ├── CoachVariant.cs        # Economy, Business
│   └── PaymentStatus.cs       # Unpaid, Paid
├── Models/
│   ├── Bus.cs                 # Coach metadata + seating capacity by variant
│   ├── Schedule.cs            # Route, departure, price, reserved-seat tracking
│   ├── User.cs                # User profile
│   ├── Ticket.cs              # Booking record (user + schedule + seat)
│   └── Invoice.cs             # Payment record tied to a Ticket
├── Interfaces/
│   ├── Repositories/          # IRepository<T>, IBusRepository,
│   │                          # IUserRepository, IScheduleRepository,
│   │                          # ITicketRepository, IInvoiceRepository
│   └── Services/              # IBusService, IUserService, IScheduleService,
│                              # IBookingService, IInvoiceService, IIdGenerator
├── Repositories/              # (planned — implementations to come)
├── Services/                  # (planned — implementations to come)
└── UI/                        # (planned — console UI to come)
```

### Domain overview

- **Bus** - identified by `BusId`, belongs to a `CoachVariant` (Economy / Business)
  which determines `TotalSeats`.
- **Schedule** - a route (`DepartureCity` -> `ArrivalCity`) on a specific
  `DepartureDateTime` operated by a `Bus`, with a `TicketPrice` and a set of
  reserved seats. Exposes seat-availability checks and a count of remaining
  seats.
- **User** - passenger profile (name, mobile, email).
- **Ticket** - a `User`'s reservation of a specific `SeatNumber` on a
  `Schedule`, stamped with `BookingDate`.
- **Invoice** - generated against a `Ticket` with an `AmountDue` and a
  `PaymentStatus`; supports `MarkAsPaid()`.

### Current status

- Models, enums, and service/repository interfaces are defined.
- Concrete repository, service, and UI implementations are not yet committed
  (`Repositories/`, `Services/`, and `UI/` are placeholder folders).
- `Program.cs` currently contains only the `AppLogger` singleton — there is no
  `Main` entry point wired up yet.

### Build & run

```bash
cd Assignment01
dotnet build
dotnet run
```

using Assignment01.Enums;
using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

namespace Assignment01.Services;

public class BusService : IBusService
{
    private readonly IBaseRepository<Bus> _busRepo;
    private readonly IIdGenerator _idGenerator;

    public BusService(IBaseRepository<Bus> busRepo, IIdGenerator idGenerator)
    {
        _busRepo = busRepo;
        _idGenerator = idGenerator;
    }

    public Bus CreateBus(string coachNumber, CoachVariant coachClass)
    {
        // Validate first so a rejected bus never consumes an ID.
        Bus.Validate(coachNumber);
        var bus = new Bus(_idGenerator.GenerateBusId(), coachNumber, coachClass);
        _busRepo.Add(bus);
        return bus;
    }

    public IEnumerable<Bus> GetAllBuses() => _busRepo.GetAll();

    public Bus? GetBusById(string busId) => _busRepo.GetById(busId);
}

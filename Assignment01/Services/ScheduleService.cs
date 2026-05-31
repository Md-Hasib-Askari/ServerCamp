using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

namespace Assignment01.Services;

public class ScheduleService : IScheduleService
{
    private readonly IBaseRepository<Schedule> _scheduleRepo;
    private readonly IBaseRepository<Bus> _busRepo;
    private readonly IIdGenerator _idGenerator;

    public ScheduleService(
        IBaseRepository<Schedule> scheduleRepo,
        IBaseRepository<Bus> busRepo,
        IIdGenerator idGenerator
    )
    {
        _scheduleRepo = scheduleRepo;
        _busRepo = busRepo;
        _idGenerator = idGenerator;
    }

    public Schedule CreateSchedule(
        string departCity,
        string arrivalCity,
        DateTime departDateTime,
        decimal ticketPrice,
        string busId
    )
    {
        var bus =
            _busRepo.GetById(busId) ?? throw new KeyNotFoundException($"Bus '{busId}' not found.");

        var schedule = new Schedule(
            _idGenerator.GenerateScheduleId(),
            departCity,
            arrivalCity,
            departDateTime,
            ticketPrice,
            bus
        );

        _scheduleRepo.Add(schedule);
        return schedule;
    }

    public IEnumerable<Schedule> GetAllSchedule() => _scheduleRepo.GetAll();

    public Schedule? GetScheduleById(string scheduleId) => _scheduleRepo.GetById(scheduleId);
}

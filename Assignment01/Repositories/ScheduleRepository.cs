using Assignment01.Interfaces;
using Assignment01.Models;

namespace Assignment01.Repositories;

public class ScheduleRepository : IBaseRepository<Schedule>
{
    private readonly Dictionary<string, Schedule> _schedules = new();

    public void Add(Schedule schedule) => _schedules[schedule.ScheduleId] = schedule;

    public IEnumerable<Schedule> GetAll() => _schedules.Values;

    public Schedule? GetById(string scheduleId) =>
        _schedules.TryGetValue(scheduleId, out var s) ? s : null;
}

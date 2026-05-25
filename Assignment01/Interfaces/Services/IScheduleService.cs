namespace Assignment01.Interfaces.Services;

using Assignment01.Models;

public interface IScheduleService
{
    Schedule CreateSchedule(
        string departCity,
        string arrivalCity,
        DateTime departDateTime,
        decimal ticketPrice,
        string busId
    );
    IEnumerable<Schedule> GetAllSchedule();
    Schedule? GetScheduleById(string scheduleId);
}

namespace Assignment01.Interfaces.Services;

using Assignment01.Enums;
using Assignment01.Models;

public interface IBusService
{
    Bus CreateBus(string coachNumber, CoachVariant coachClass);
    IEnumerable<Bus> GetAllBuses();
    Bus? GetBusById(string busId);
}

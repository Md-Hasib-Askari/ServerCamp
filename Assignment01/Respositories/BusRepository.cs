using Assignment01.Interfaces;
using Assignment01.Models;

namespace Assignment01.Services;

public class BusRepository : IBaseRepository<Bus>
{
    private readonly Dictionary<string, Bus> _buses = new();

    public void Add(Bus bus) => _buses[bus.BusId] = bus;

    public bool Exists(string busId) => _buses.ContainsKey(busId);

    public IEnumerable<Bus> GetAll() => _buses.Values;

    public Bus? GetById(string busId) => _buses.TryGetValue(busId, out var b) ? b : null;
}

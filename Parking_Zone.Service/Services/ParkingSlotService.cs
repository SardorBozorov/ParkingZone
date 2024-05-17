using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class ParkingSlotService : Service<ParkingSlot>, IParkingSlotService
{
    private readonly IParkingSlotRepository _parkingSlotRepository;
    public ParkingSlotService(IParkingSlotRepository repository) : base(repository)
    {
        _parkingSlotRepository = repository;
    }

    public IEnumerable<ParkingSlot> GetSlotsByZoneId(long parkingZoneId)
    {
        return _parkingSlotRepository.GetAll().Where(x => x.ParkingZoneId == parkingZoneId).ToList();
    }
    public bool IsUniqueNumber(long zoneId, uint Number)
    {
        var existSlots = GetSlotsByZoneId(zoneId).Any(s => s.Number == Number);
        return existSlots;
    }

}

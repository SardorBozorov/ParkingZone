using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Service.Interfaces;

public interface IParkingSlotService : IService<ParkingSlot>
{
    public IEnumerable<ParkingSlot> GetSlotsByZoneId(long parkingZoneId);
}

using Parking_Zone.Domain.Commons;

namespace Parking_Zone.Domain.Entities;

public class ParkingZone : Auditable
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Street { get; set; }
}

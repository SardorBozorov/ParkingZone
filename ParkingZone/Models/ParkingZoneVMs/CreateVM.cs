using Parking_Zone.Domain.Entities;
using System.Net;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs;

public class CreateVM
{
    public string Name { get; set; }
    public string Address { get; set; }

    public ParkingZone MapToModel()
    {
        return new ParkingZone
        {
            Name = Name,
            Address = Address
        };
    }
}

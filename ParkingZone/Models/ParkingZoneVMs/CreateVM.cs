using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs;

public class CreateVM
{
    [Required]
    public string Name { get; set; }
    [Required]
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

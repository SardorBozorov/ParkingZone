using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingSlotVMs;

public class CreateVM
{
    [Required]
    public uint Number { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [Required]
    public ParkingSlotCategory Category { get; set; }
    [Required]
    public long ParkingZoneId { get; set; }

    public ParkingSlot MapToModel()
    {
        return new ParkingSlot
        {
            Number = Number,
            IsAvailable = IsAvailable,
            Category = Category,
            ParkingZoneId = ParkingZoneId
        };
    }
}

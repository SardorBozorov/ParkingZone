using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingSlotVMs;

public class DetailsVM
{
    [Required]
    public long Id { get; set; }
    [Required]
    public uint Number { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [Required]
    public ParkingSlotCategory Category { get; set; }
    [Required]
    public long ParkingZoneId { get; set; }

    public DetailsVM() {}

    public DetailsVM(ParkingSlot slot)
    {
        Id = slot.Id;
        Number = slot.Number;
        IsAvailable = slot.IsAvailable;
        Category = slot.Category;
        ParkingZoneId = slot.ParkingZoneId;
    }
}

using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingSlotVMs;

public class ListOfSlotsVM
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Number { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [Required]
    public ParkingSlotCategory Category { get; set; }
    [Required]
    public long ParkingZoneId { get; set; }
    public ListOfSlotsVM()
    { }

    public ListOfSlotsVM(ParkingSlot slot)
    {
        Id = slot.Id;
        Number = slot.Number;
        Category = slot.Category;
        ParkingZoneId = slot.ParkingZoneId;
        IsAvailable = slot.IsAvailable;
    }

    public static IEnumerable<ListOfSlotsVM> MapToVM(IEnumerable<ParkingSlot> slots)
    {
        return slots.Select(slot => new ListOfSlotsVM(slot));
    }
}

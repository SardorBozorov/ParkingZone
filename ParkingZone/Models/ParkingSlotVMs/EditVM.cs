using Parking_Zone.Domain.Entities;
using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingSlotVMs;

public class EditVM
{
    [Required]
    public uint Number { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [Required]
    public ParkingSlotCategory Category { get; set; }
    [Required]
    public long ParkingZoneId { get; set; }
    public long Id { get; set; }
    public EditVM()
    { }
    public EditVM(ParkingSlot parkingSlot)
    {
        Number = parkingSlot.Number;
        Category = parkingSlot.Category;
        IsAvailable = parkingSlot.IsAvailable;
        ParkingZoneId = parkingSlot.ParkingZoneId;
        Id = parkingSlot.Id;
    }
    public ParkingSlot MapToModel(ParkingSlot Vm)
    {
        Vm.Number = Number;
        Vm.Category = Category;
        Vm.IsAvailable = IsAvailable;
        Vm.ParkingZoneId = ParkingZoneId;
        Vm.Id = Id;
        return Vm;
    }
}


using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs;

public class EditVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public EditVM()
    { }
    public EditVM(ParkingZone parkingZone)
    {
        Name = parkingZone.Name;
        Address = parkingZone.Address;
    }
    public ParkingZone MapToModel(ParkingZone Vm)
    {
        Vm.Name = Name;
        Vm.Address = Address;
        return Vm;
    }
}

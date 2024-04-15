
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs;

public class EditVM
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public EditVM()
    { }
    public EditVM(ParkingZone parkingZone)
    {
        Id = parkingZone.Id;
        Name = parkingZone.Name;
        Address = parkingZone.Address;
    }
    public ParkingZone MapToModel(ParkingZone Vm)
    {
        Vm.Id = Id;
        Vm.Name = Name;
        Vm.Address = Address;
        return Vm;
    }
}

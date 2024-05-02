using Parking_Zone.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs;

public class ListOfItemsVM
{
    [Required]
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public DateTime? DateOfEstablishment { get; set; }
    public ListOfItemsVM()
    {}

    public ListOfItemsVM(ParkingZone parkingZone)
    {
        Id = parkingZone.Id;
        Name = parkingZone.Name;
        Address = parkingZone.Address;
        DateOfEstablishment = parkingZone.DateOfEstablishment;
    }
}

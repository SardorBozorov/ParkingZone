using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs
{
    public class ListOfItemsVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } 
        public DateTime DateOfEstablishment { get; set; }

        public ListOfItemsVM(ParkingZone parkingZone)
        {
            Id = parkingZone.Id;
            Name = parkingZone.Name;
            Address = parkingZone.Address;
            DateOfEstablishment = parkingZone.DateOfEstablishment;
        }

    }
}

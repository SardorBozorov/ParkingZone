using NuGet.Packaging.Signing.DerEncoding;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.Models.ParkingZoneVMs
{
    public class DetailsVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfEstablishment { get; set; }

        public DetailsVM()
        { }
        public DetailsVM(ParkingZone parkingZone)
        {
            Id = parkingZone.Id;
            Name = parkingZone.Name;
            Address = parkingZone.Address;
            DateOfEstablishment = parkingZone.DateOfEstablishment;
        }
    }
}

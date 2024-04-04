using ParkingZone.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingZone.Domain.Entities;

public class ParkingZoneModel : Auditable
{
    public string Name { get; set; }
     public string Country { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Street { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Domain.Entities;

public class ParkingZone
{
    [Key]
    [Required]
    public long Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public DateTime DateOfEstablishment { get; set; } = DateTime.UtcNow;
}

using Parking_Zone.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Parking_Zone.Domain.Entities;

public class ParkingSlot
{
    [Key]
    [Required]
    public long Id { get; set; }
    [Required]
    public string Number { get; set; }
    [Required]
    public bool IsAvailable { get; set; }
    [Required]
    public ParkingSlotCategory Category { get; set; }
    [Required]
    public ParkingZone Zone { get; set; }
    [Required]
    public long ParkingZoneId { get; set; }
}

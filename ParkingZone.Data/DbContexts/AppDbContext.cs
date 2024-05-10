using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Domain.Entities;


namespace Parking_Zone.Data.DbCondext;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ParkingZone> ParkingZones { get; set; }
    public DbSet<ParkingSlot> ParkingSlots { get; set;}
}


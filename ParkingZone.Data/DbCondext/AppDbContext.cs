using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingZone.Domain.Entities;

namespace ParkingZone.Data.DbCondext
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ParkingZoneModel> ParkingZoneModels { get; set; }
    }
}


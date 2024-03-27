﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParkingZone.Models;

namespace ParkingZone.Data;

public class ParkingZoneDbContext : IdentityDbContext
{
    public ParkingZoneDbContext(DbContextOptions<ParkingZoneDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<PparkingZone> ParkingZones { get; set; } = default!;
}

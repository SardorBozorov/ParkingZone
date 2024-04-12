﻿using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class ParkingZoneService : Service<ParkingZone>, IParkingZoneService
{
    public ParkingZoneService(IParkingZoneRepository repository) : base(repository)
    {
    }

    public override void Create(ParkingZone parkingZone)
    {
        base.Create(parkingZone);
    }
}

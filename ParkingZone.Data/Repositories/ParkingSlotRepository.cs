﻿using Parking_Zone.Data.DbCondext;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.Data.Repositories;

public class ParkingSlotRepository : Repository<ParkingSlot>, IParkingSlotRepository
{
    public ParkingSlotRepository(AppDbContext context) : base(context)
    { }
}

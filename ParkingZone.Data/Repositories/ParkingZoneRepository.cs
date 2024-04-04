using ParkingZone.Data.DbCondext;
using ParkingZone.Data.IRepositories;
using ParkingZone.Domain.Entities;

namespace ParkingZone.Data.Repositories;

public class ParkingZoneRepository : Repository<Domain.Entities.ParkingZone> , IParkingZoneRepository
{
    public ParkingZoneRepository(AppDbContext options) : base(options)
    { }
}

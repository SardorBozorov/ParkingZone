using ParkingZone.Data.DbCondext;
using ParkingZone.Data.IRepositories;
using ParkingZone.Domain.Entities;

namespace ParkingZone.Data.Repositories;

public class ParkingZoneModelRepository : GenericRepository<ParkingZoneModel> , IParkingZoneModelRepository
{
    public ParkingZoneModelRepository(AppDbContext options) : base(options)
    { }
}

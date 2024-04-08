using NuGet.Protocol.Core.Types;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.Extensions;

public static class ServiceExtentions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();
    }
}

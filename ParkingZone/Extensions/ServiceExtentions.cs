using NuGet.Protocol.Core.Types;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Data.Repositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.Service.Interfaces;
using Parking_Zone.Service.Services;

namespace Parking_Zone.MVC.Extensions;

public static class ServiceExtentions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        // Repository
        services.AddScoped<IParkingZoneRepository, ParkingZoneRepository>();

        // Service
        services.AddScoped<IParkingZoneService, ParkingZoneService>();
    }
}

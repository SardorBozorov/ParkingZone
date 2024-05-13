using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ParkingSlotController : Controller
{
    private readonly IParkingSlotService _parkingSlotService;
    public ParkingSlotController(IParkingSlotService parkingSlotService)
    {
        _parkingSlotService = parkingSlotService;
    }

    public IActionResult Index(long parkingZoneId)
    {
        var parkingSlots = _parkingSlotService.GetSlotsByZoneId(parkingZoneId);
        var listItemVMs = ListOfSlotsVM.MapToVM(parkingSlots);
        return View(listItemVMs);
    }
}

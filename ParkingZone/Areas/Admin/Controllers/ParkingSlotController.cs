﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Zone.MVC.Models.ParkingSlotVMs;
using Parking_Zone.Service.Interfaces;
using System.Security.Policy;

namespace Parking_Zone.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class ParkingSlotController : Controller
{
    private readonly IParkingSlotService _parkingSlotService;
    private readonly IParkingZoneService _parkingZoneService;
    public ParkingSlotController(IParkingSlotService parkingSlotService, IParkingZoneService parkinZoneService)
    {
        _parkingSlotService = parkingSlotService;
        _parkingZoneService = parkinZoneService;
    }

    public IActionResult Index(long zoneId)
    {
        var parkingSlots = _parkingSlotService.GetSlotsByZoneId(zoneId);
        var listItemVMs = ListOfSlotsVM.MapToVM(parkingSlots).ToList();
        var zone = _parkingZoneService.GetById(zoneId);
        ViewData["parkingZoneId"] = zoneId;
        ViewData["name"] = zone.Name;
        return View(listItemVMs);
    }

    public IActionResult Create(long parkingZoneId)
    {
        var existParkingZone = _parkingZoneService.GetById(parkingZoneId);
        if (existParkingZone != null)
        {
            ViewData["name"] = existParkingZone.Name;
            CreateVM createVM = new()
            {
                ParkingZoneId = parkingZoneId,
            };
            return View(createVM);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateVM createVM)
    {
        if (_parkingSlotService.IsUniqueNumber(createVM.ParkingZoneId, createVM.Number))
        {
            ModelState.AddModelError("Number", "Slot number already exists in this zone");
        }
        if (ModelState.IsValid)
        {
            _parkingSlotService.Create(createVM.MapToModel());
            return RedirectToAction(nameof(Index), new {zoneId = createVM.ParkingZoneId});
        }
        return View(createVM);
    }
}

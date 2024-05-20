using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parking_Zone.Domain.Entities;
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

    [HttpGet]
    public IActionResult Edit(long id)
    {
        var slot = _parkingSlotService.GetById(id);

        if (slot is null)
            return NotFound();

        var editVM = new EditVM(slot);
        return View(editVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditVM editVM, long id)
    {
        if (id != editVM.Id)
            return NotFound();
        var slot = _parkingSlotService.GetById(id);

        if (slot is null)
            return NotFound();

        if (_parkingSlotService.IsUniqueNumber(editVM.ParkingZoneId, editVM.Number) && slot.Number != editVM.Number)
        {
            ModelState.AddModelError("Number", "The parking slot number is not unique");
        }

        if (ModelState.IsValid)
        {
            slot = editVM.MapToModel(slot);
            _parkingSlotService.Update(slot);
            return RedirectToAction("Index", new { zoneId = slot.ParkingZoneId });
        }
        return View(editVM);
    }

    [HttpGet]
    public IActionResult Details(long id)
    {
        if (id == 0)
            return NotFound();

        var existSlot = _parkingSlotService.GetById(id);
        if (existSlot is null)
        {
            return NotFound();
        }
        DetailsVM detailsVM = new(existSlot);
        return View(detailsVM);
    }
}

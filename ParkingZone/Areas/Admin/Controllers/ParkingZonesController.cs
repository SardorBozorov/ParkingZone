using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;
using Parking_Zone.MVC.Models.ParkingZoneVMs;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParkingZonesController : Controller
    {
        private readonly IParkingZoneService _service;

        public ParkingZonesController(IParkingZoneService service)
        {
            _service = service;
        }

        // GET: Admin/ParkingZones
        public IActionResult Index()
        {
            var result = _service.GetAll();
            var VMs = result.Select(x => new ListOfItemsVM(x));
            return View(VMs);
        }

        // GET: Admin/ParkingZones/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _service.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            var detailsVM = new DetailsVM(parkingZone);

            return View(detailsVM);
        }

        // GET: Admin/ParkingZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ParkingZones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateVM createVM)
        {
            if (ModelState.IsValid)
            {
                _service.Create(createVM.MapToModel());
                return RedirectToAction(nameof(Index));
            }
            return View(createVM);
        }

        // GET: Admin/ParkingZones/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _service.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            var VM = new EditVM(parkingZone);
            return View(VM);
        }

        // POST: Admin/ParkingZones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, EditVM parkingZoneVM)
        {
            var parkingZone = _service.GetById(id);
            if (id != parkingZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(parkingZoneVM.MapToModel(parkingZone));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingZoneExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZoneVM);
        }

        // GET: Admin/ParkingZones/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _service.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var parkingZone = _service.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            _service.Delete(parkingZone);
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingZoneExists(long id)
        {
            var ParkingZone = _service.GetById(id);
            return true ? ParkingZone != null : false;
        }
    }
}

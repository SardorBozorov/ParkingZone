using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Entities;

namespace Parking_Zone.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParkingZonesController : Controller
    {
        private readonly IParkingZoneRepository _repository;

        public ParkingZonesController(IParkingZoneRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin/ParkingZones
        public IActionResult Index()
        {
            var result = _repository.GetAll().ToList();
            return View(result);
        }

        // GET: Admin/ParkingZones/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _repository.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
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
        public IActionResult Create(ParkingZone parkingZone)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(parkingZone);
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _repository.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }
            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id,ParkingZone parkingZone)
        {
            if (id != parkingZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _repository.Update(parkingZone);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkingZoneExists(parkingZone.Id))
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
            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = _repository.GetById(id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(long id)
        {
            var parkingZone = _repository.GetById(id);
            if (parkingZone != null)
            {
                _repository.Delete(parkingZone);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ParkingZoneExists(long id)
        {
            var ParkingZone = _repository.GetById(id);
            return true ? ParkingZone != null : false;
        }
    }
}

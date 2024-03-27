using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingZone.Data;
using ParkingZone.Models;

namespace ParkingZone.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ParkingZonesController : Controller
    {
        private readonly ParkingZoneDbContext _context;

        public ParkingZonesController(ParkingZoneDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ParkingZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkingZones.ToListAsync());
        }

        // GET: Admin/ParkingZones/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Country,City,Region,Street")] PparkingZone parkingZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(parkingZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkingZone);
        }

        // GET: Admin/ParkingZones/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones.FindAsync(id);
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
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Country,City,Region,Street")] PparkingZone parkingZone)
        {
            if (id != parkingZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkingZone);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkingZone = await _context.ParkingZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkingZone == null)
            {
                return NotFound();
            }

            return View(parkingZone);
        }

        // POST: Admin/ParkingZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var parkingZone = await _context.ParkingZones.FindAsync(id);
            if (parkingZone != null)
            {
                _context.ParkingZones.Remove(parkingZone);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParkingZoneExists(long id)
        {
            return _context.ParkingZones.Any(e => e.Id == id);
        }
    }
}

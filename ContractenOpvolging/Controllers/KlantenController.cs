using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models.ContractenModels;
using Microsoft.AspNetCore.Authorization;

namespace ContractenOpvolging.Controllers
{
    [Authorize]
    public class KlantenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KlantenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Klanten
        public async Task<IActionResult> Index()
        {
            ViewBag.TargetController = "Klanten";
            return View(await _context.Klanten.ToListAsync());
        }

        // GET: Klanten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .SingleOrDefaultAsync(m => m.KlantID == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klanten/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klanten/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("KlantID,Naam,Contactpersoon,Telefoon,Email,Straat,Huisnummer,Postcode,Gemeente")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(klant);
        }

        // GET: Klanten/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten.SingleOrDefaultAsync(m => m.KlantID == id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klanten/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("KlantID,Naam,Contactpersoon,Telefoon,Email,Straat,Huisnummer,Postcode,Gemeente")] Klant klant)
        {
            if (id != klant.KlantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantExists(klant.KlantID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(klant);
        }

        // GET: Klanten/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klanten
                .SingleOrDefaultAsync(m => m.KlantID == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klanten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klant = await _context.Klanten.SingleOrDefaultAsync(m => m.KlantID == id);
            _context.Klanten.Remove(klant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool KlantExists(int id)
        {
            return _context.Klanten.Any(e => e.KlantID == id);
        }

        [HttpPost]
        public async Task<IActionResult> KlantZoeken(string query)
        {
            if (query != null)
            {
                var model = await _context.Klanten
                                          .Where(k => k.Naam.ToLower().StartsWith(query.ToLower()))
                                          .OrderBy(n => n.Naam)
                                          .ToListAsync();

                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}

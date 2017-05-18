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
    public class ConsultantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsultantsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Consultants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Consultants.ToListAsync());
        }

        // GET: Consultants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .SingleOrDefaultAsync(m => m.ConsultantID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }
        [Authorize(Roles ="Admin")]
        // GET: Consultants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ConsultantID,Voornaam,Familienaam,Telefoon,Email,Soort")] Consultant consultant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultant);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(consultant);
        }

        // GET: Consultants/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants.SingleOrDefaultAsync(m => m.ConsultantID == id);
            if (consultant == null)
            {
                return NotFound();
            }
            return View(consultant);
        }

        // POST: Consultants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsultantID,Voornaam,Familienaam,Telefoon,Email,Soort")] Consultant consultant)
        {
            if (id != consultant.ConsultantID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultantExists(consultant.ConsultantID))
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
            return View(consultant);
        }

        // GET: Consultants/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultant = await _context.Consultants
                .SingleOrDefaultAsync(m => m.ConsultantID == id);
            if (consultant == null)
            {
                return NotFound();
            }

            return View(consultant);
        }

        // POST: Consultants/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consultant = await _context.Consultants.SingleOrDefaultAsync(m => m.ConsultantID == id);
            _context.Consultants.Remove(consultant);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ConsultantZoeken(string query)
        {
            if (query != null)
            {
                var model = await _context.Consultants
                                          .Where(c => c.Familienaam.StartsWith(query))
                                          .OrderBy(c => c.Familienaam)
                                          .ToListAsync();

                return View("Index", model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        private bool ConsultantExists(int id)
        {
            return _context.Consultants.Any(e => e.ConsultantID == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models.ContractenModels;

namespace ContractenOpvolging.Controllers
{
    public class ContractenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractenController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Contracten
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Contracten.Include(c => c.Consultant).Include(c => c.Klant);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Contracten/Create
        public IActionResult Create()
        {
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Familienaam");
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam");
            return View();
        }

        // POST: Contracten/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractID,StartDatum,EindDatum,Opzegtermijn,Randvoorwaarden,Tarief,Kosten,Verlenging,KlantID,OnderKlant,ConsultantID")] Contract contract)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contract);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Familienaam", contract.ConsultantID);
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", contract.KlantID);
            return View(contract);
        }

        // GET: Contracten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracten.SingleOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Familienaam", contract.ConsultantID);
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", contract.KlantID);
            return View(contract);
        }

        // POST: Contracten/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContractID,StartDatum,EindDatum,Opzegtermijn,Randvoorwaarden,Tarief,Kosten,Verlenging,KlantID,OnderKlant,ConsultantID")] Contract contract)
        {
            if (id != contract.ContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractExists(contract.ContractID))
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
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Familienaam", contract.ConsultantID);
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", contract.KlantID);
            return View(contract);
        }

        // GET: Contracten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contract = await _context.Contracten
                .Include(c => c.Consultant)
                .Include(c => c.Klant)
                .SingleOrDefaultAsync(m => m.ContractID == id);
            if (contract == null)
            {
                return NotFound();
            }

            return View(contract);
        }

        // POST: Contracten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contract = await _context.Contracten.SingleOrDefaultAsync(m => m.ContractID == id);
            _context.Contracten.Remove(contract);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContractExists(int id)
        {
            return _context.Contracten.Any(e => e.ContractID == id);
        }
    }
}

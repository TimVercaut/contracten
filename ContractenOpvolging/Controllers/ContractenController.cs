using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContractenOpvolging.Data;
using ContractenOpvolging.Models.ContractenModels;
using Microsoft.AspNetCore.Authorization;
using ContractenOpvolging.Models;

namespace ContractenOpvolging.Controllers
{
    [Authorize]
    public class ContractenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractenController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<List<Contract>> GetContractenByDate()
        {
            return await _context.Contracten.OrderBy(c => c.EindDatum)
                                            .Include(c => c.Consultant)
                                            .Include(c => c.Klant)
                                            .ToListAsync();
        }

        private async Task<List<Klant>> GetKlanten()
        {
            return await _context.Klanten.ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByName(string query = "")
        {
            return await _context.Contracten.Where(c => c.Consultant.Familienaam.StartsWith(query))
                                            .OrderBy(c => c.Consultant.Familienaam)
                                            .Include(c => c.Consultant)
                                            .Include(c => c.Klant)
                                            .ToListAsync();
        }

        // GET: Contracten
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                RedirectToAction("ContractenDetails");
            }
            ViewBag.KlantenLijst = await GetKlanten();
            return View(await GetContractenByName());
        }

        public async Task<IActionResult> Grafisch()
        {
            ViewBag.KlantenLijst = await GetKlanten();
            ViewBag.Maanden = 6;
            return View(await GetContractenByDate());
        }

        [HttpPost]
        public async Task<IActionResult> MaandenAanpassen(string query)
        {
            int maanden;
            if (int.TryParse(query, out maanden))
            {
                if (maanden >= 15) { maanden = 15; } //Never show more than 15 months
                ViewBag.Maanden = maanden;
                ViewBag.KlantenLijst = await GetKlanten();
                return View("Grafisch", await GetContractenByDate());
            }
            else
            {
                return RedirectToAction("Grafisch");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ConsultantZoeken(string query, string controller = "Contracten")
        {
            if (query != null)
            {
                var target = ZoekTargetPagina();
                ViewBag.KlantenLijst = await GetKlanten();
                return View(target, await GetContractenByName(query));
            }
            else
            {
                return RedirectToAction("Index", controller);
            }
        }

        [HttpPost]
        public async Task<IActionResult> KlantZoeken(string query)
        {
            if (query != null)
            {
                var target = ZoekTargetPagina();
                ViewBag.KlantenLijst = await GetKlanten();
                var model = _context.Contracten
                                    .Where(c => c.Klant.Naam.StartsWith(query))
                                    .OrderBy(c => c.Consultant.Familienaam)
                                    .Include(c => c.Consultant)
                                    .Include(c => c.Klant)
                                    .ToListAsync();
                return View(target, await model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Contracten/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Naam");
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam");
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Verleng(int id)
        {
            var model = new ContractVerlengingViewModel();
            
            if (ContractExists(id))
            {
                var contract = await _context.Contracten
                                    .Include("Consultant")
                                    .Include("Klant")
                                    .Where(ID => ID.ContractID == id)
                                    .FirstOrDefaultAsync();

                model.Consultant = contract.Consultant.Naam;
                model.Klant = contract.Klant.Naam;
                model.ContractID = contract.ContractID;
                model.EindDatum = contract.EindDatum;
                model.NieuweEindDatum = contract.EindDatum;
                model.VerlengKleur = contract.Verlenging;
                ViewBag.Lijst = new SelectList(
                    Enum.GetValues(typeof(Verlenging)).Cast<Verlenging>(), nameof(contract.Verlenging));
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Verleng"),Authorize(Roles ="Admin")]
        public async Task<IActionResult> VerlengContract(int id,[Bind("NieuweEindDatum","NieuweKleur")] ContractVerlengingViewModel model)
        {
            var contract = await _context.Contracten.FindAsync(id);
            if (contract != null)
            {
                var kleur = model.NieuweKleur;
                contract.EindDatum = model.NieuweEindDatum;
                if (model.NieuweKleur != null)
                {
                    contract.Verlenging = (Verlenging)Enum.Parse(typeof(Verlenging), kleur, true);
                }
                _context.SaveChanges();
                return RedirectToAction("ContractenDetails");
            }
            else
            {
                return View("Verleng", model);
            }
        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> ContractenDetails()
        {
            ViewBag.KlantenLijst = await GetKlanten();
            var contracten = await GetContractenByName();
            return View(contracten);
        }

        private string ZoekTargetPagina()
        {
            if (User.IsInRole("Admin"))
            {
                return "ContractenDetails";
            }
            else return "Index";
        }
    }
}

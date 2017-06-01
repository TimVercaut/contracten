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

        private IQueryable<Contract> ContractenQuery(string query = "")
        {
            return  _context.Contracten.Include(c => c.Consultant)
                                       .Include(c => c.Klant);
        }

        private async Task<List<Contract>> GetContractenByDate()
        {

            return await ContractenQuery().OrderBy(c => c.EindDatum)
                                          .ToListAsync();
        }

        private async Task<List<Contract>> GetContractenByDateDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.EindDatum)
                                          .ToListAsync();
        }

        private async Task<List<Klant>> GetKlanten()
        {
            return await _context.Klanten.ToListAsync();
        }

        private async Task<List<Contract>> GetContractenByNameDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.Consultant.Familienaam)
                                          .ToListAsync();
        }

        private async Task<List<Contract>> GetContractenByKlant()
        {
            return await ContractenQuery().OrderBy(c => c.Klant.Naam)
                                          .ToListAsync();
        }

        private async Task<List<Contract>> GetcontractenByKlantDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.Klant.Naam)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByName(string query = "")
        {

            return await ContractenQuery().Where(c => c.Consultant.Familienaam.StartsWith(query))
                                          .OrderBy(c => c.Consultant.Familienaam)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByTarief()
        {
            return await ContractenQuery().OrderBy(c => c.Tarief)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByTariefDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.Tarief)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByKost()
        {
            return await ContractenQuery().OrderBy(c => c.Kosten)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByKostDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.Kosten)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByVerlenging()
        {
            return await ContractenQuery().OrderBy(c => c.Verlenging)
                                          .ThenBy(c => c.Consultant.Familienaam)
                                          .ToListAsync();
        }

        public async Task<List<Contract>> GetContractenByVerlengingDesc()
        {
            return await ContractenQuery().OrderByDescending(c => c.Verlenging)
                                          .ThenBy(c => c.Consultant.Familienaam)
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
                if (ConsultantHasContract(contract.ConsultantID))
                {
                    _context.Add(contract);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("HasContract", contract.ConsultantID);
                }
            }
            ViewData["ConsultantID"] = new SelectList(_context.Consultants, "ConsultantID", "Familienaam", contract.ConsultantID);
            ViewData["KlantID"] = new SelectList(_context.Klanten, "KlantID", "Naam", contract.KlantID);
            return View(contract);
        }

        public IActionResult HasContract(int id)
        {

            return View();
        }

        private bool ConsultantHasContract(int id)
        {
            var contract = (from con in _context.Contracten
                           where con.ConsultantID == id
                           select con).FirstOrDefault();
            return (contract == null);
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

        [HttpPost, ActionName("Verleng"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> VerlengContract(int id, [Bind("NieuweEindDatum", "NieuweKleur")] ContractVerlengingViewModel model)
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ContractenDetails(string id)
        {
            //viewbags voor filters en voor de business partner
            ViewBag.KlantenLijst = await GetKlanten();
            ViewBag.NameSortPar = string.IsNullOrEmpty(id) ? "name_desc" : "";
            ViewBag.DateSortPar = id == "date" ? "date_desc" : "date";
            ViewBag.KlantSorPar = id == "klant" ? "klant_desc" : "klant";
            ViewBag.TariefSorPar = id == "tarief" ? "tarief_desc" : "tarief";
            ViewBag.KostenSorPar = id == "kosten" ? "kosten_desc" : "kosten";
            ViewBag.VerlengSorPar = id == "verleng" ? "verleng_desc" : "verleng";
            List<Contract> contracten;
            if (id == null)
            {
                 contracten = await GetContractenByName();
            }
            else
            {
                 contracten = await SorteerDeLijstAsync(id);
            }
            return View(contracten);
        }

        private async Task<List<Contract>> SorteerDeLijstAsync(string sortId)
        {
            //switch voor de filterparameters
            List<Contract> contracten;
            switch (sortId)
            {
                case "name_desc":
                    contracten = await GetContractenByNameDesc();
                    break;

                case "date":
                    contracten = await GetContractenByDate();
                    break;

                case "date_desc":
                    contracten = await GetContractenByDateDesc();
                    break;

                case "klant":
                    contracten = await GetContractenByKlant();
                    break;

                case "klant_desc":
                    contracten = await GetcontractenByKlantDesc();
                    break;

                case "tarief":
                    contracten = await GetContractenByTarief();
                    break;

                case "tarief_desc":
                    contracten = await GetContractenByTariefDesc();
                    break;

                case "kosten":
                    contracten = await GetContractenByKost();
                    break;

                case "kosten_desc":
                    contracten = await GetContractenByKostDesc();
                    break;

                case "verleng":
                    contracten = await GetContractenByVerlenging();
                    break;

                case"verleng_desc":
                    contracten = await GetContractenByVerlengingDesc();
                    break;

                default:
                    contracten = await GetContractenByName();
                    break;
            }
            return contracten;
        }

        private string ZoekTargetPagina()
        {
            if (User.IsInRole("Admin"))
            {
                return "ContractenDetails";
            }
            else return "Index";
        }

        public IActionResult Archive(int id)
        {
            var contract = _context.Contracten
                                   .Include("Consultant")
                                   .Include("Klant")
                                   .Where(c => c.ContractID == id)
                                   .FirstOrDefault();
            if (contract != null)
            {
                return View(contract);
            }
            return RedirectToAction("ContractenDetails");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmArchive(int? id)
        {
            if (id != null)
            {
                var contract = _context.Contracten
                                       .Include("Klant")
                                       .Include("Consultant")
                                       .Where(i => i.ContractID == id).FirstOrDefault();
                if (contract != null)
                {
                    var thisYear = DateTime.Now.Year.ToString();
                    //voeg het contract toe aan het archief
                    var oudContract = new OudContract();

                    oudContract.Jaar = thisYear;
                    oudContract.Consultant = contract.Consultant.Naam.ToString();
                    oudContract.Klant = contract.Klant.Naam;
                    if (contract.OnderKlant != null)
                    {
                        oudContract.Subklant = _context.Klanten.Find(contract.OnderKlant).Naam;
                    }
                    else
                    {
                        oudContract.Subklant = null;
                    }
                    oudContract.StartDatum = contract.StartDatum;
                    oudContract.EindDatum = contract.EindDatum;
                    oudContract.Tarief = contract.Tarief;
                    oudContract.Kost = contract.Kosten;
                    try
                    {
                        //probeer het op te slaan en te verwijderen uit de actieve db
                        _context.OudeContracten.Add(oudContract);
                        _context.Contracten.Remove(contract);
                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Error");
                    }
                }
            }
            return RedirectToAction("OudeContracten","Admin");
        }
    }
}

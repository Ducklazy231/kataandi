using System;
using System.Linq;
using System.Threading.Tasks;
using Kataandi.Models;
using Kataandi.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kataandi.Controllers
{
    public class AsetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aset/Index
        public async Task<IActionResult> Index()
        {
            // Fetch the list of assets
            //var assets = await _context.Aset.FromSqlRaw("SELECT * FROM master.dbo.[MD_Aset];").ToListAsync();
            //List<Aset> assets = await _context.Aset.FromSqlRaw("SELECT * FROM master.dbo.[MD_Aset];").ToListAsync();
            var newAset = new Aset();
            var viewAsset = new AsetView();
            //viewAsset.newAset = newAset;
            //viewAsset.assets = assets;
            viewAsset.newAset = new MD_Aset();
            viewAsset.assets = await _context.MD_Aset.ToListAsync();

            // Pass the tuple to the view
            return View(viewAsset);
        }

        // GET: Aset/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asset = await _context.MD_Aset.FirstOrDefaultAsync(m => m.LineNo == id);
            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        // GET: Aset/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aset/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MD_Aset newAset)
        {
            if (ModelState.IsValid)
            {
                _context.MD_Aset.Add(newAset);
                await _context.SaveChangesAsync();
                var updatedAssets = _context.MD_Aset.ToList();
                return Json(new { success = true, message = "Data has been saved successfully" });
                
            }
            else
            {
                 
            }
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Json(new { success = false, errors = errors });
            }
        }


        // get: Aset/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(string AsetNo)
        {
            
            if (AsetNo == null)
            {
                return NotFound();
            }

            var aset = await _context.MD_Aset.FirstOrDefaultAsync(m => m.AsetNo == AsetNo);
            if (aset == null)
            {
                return NotFound();
            }


            
            return View(aset);
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditConfirmation(Aset asetParam)
        {
            if (asetParam == null)
            {
                return NotFound();
            }

            // Fetch the existing record from the database
            var aset = await _context.MD_Aset.FindAsync(asetParam.AsetNo);
            if (aset == null)
            {
                return Json("Asset not found");
            }

            // Apply the updated values from asetParam to the existing asset
            aset.AsetNo = asetParam.AsetNo;
            aset.LineNo = asetParam.LineNo;  
            aset.LocationId = asetParam.LocationId;
            aset.NomorAsetMySAP = asetParam.NomorAsetMySAP;
            aset.MySAPLineNo = asetParam.MySAPLineNo;
            aset.HarmoniNo = asetParam.HarmoniNo;
            aset.SinasNo = asetParam.SinasNo;
            aset.Deskripsi = asetParam.Deskripsi;
            aset.Merk = asetParam.Merk;
            aset.KategoriAset = asetParam.KategoriAset;
            aset.TahunPerolehan = asetParam.TahunPerolehan;
            aset.Amount = asetParam.Amount;
            aset.StatusPenggunaan = asetParam.StatusPenggunaan;
            aset.Level = asetParam.Level;
            aset.KondisiAset = asetParam.KondisiAset;
            aset.LokasiAset = asetParam.LokasiAset;
            aset.PathFotoTagging = asetParam.PathFotoTagging;
            aset.PathFotoKeseluruhan = asetParam.PathFotoKeseluruhan;

            // Update the entity in the context
            _context.MD_Aset.Update(aset);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(asetParam.AsetNo))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Aset/Delete/5
        public async Task<IActionResult> Delete(string Aset_No)
        {
            if (Aset_No == null)
            {
                return NotFound();
            }

            var aset = await _context.MD_Aset.FirstOrDefaultAsync(m => m.AsetNo == Aset_No);
            if (aset == null)
            {
                return NotFound();
            }



            return View(aset);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Aset_No)
        {
            if (Aset_No.IsNullOrEmpty())  // Updated check for int type id
            {
                return NotFound();
            }

            var aset = await _context.MD_Aset.FindAsync(Aset_No);
            if (aset == null)
            {
                return Json("Asset not found");
            }

            _context.MD_Aset.Remove(aset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(string id)
        {
            return _context.MD_Aset.Any(e => e.AsetNo == id);
        }
    }
}

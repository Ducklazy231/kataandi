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
            var assets = await _context.Aset.FromSqlRaw("SELECT * FROM master.dbo.[Aset];").ToListAsync();
            var newAset = new Aset();
            var viewAsset = new AsetView();
            viewAsset.newAset = newAset;
            viewAsset.assets = assets;

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

            var asset = await _context.Aset.FirstOrDefaultAsync(m => m.Line_No == id);
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
        public async Task<IActionResult> Create(Aset newAset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newAset);
                await _context.SaveChangesAsync();
                var updatedAssets = _context.Aset.ToList();
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
        public async Task<IActionResult> Edit(string Aset_No)
        {
            
            if (Aset_No == null)
            {
                return NotFound();
            }

            var aset = await _context.Aset.FirstOrDefaultAsync(m => m.Aset_No == Aset_No);
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
            var aset = await _context.Aset.FindAsync(asetParam.Aset_No);
            if (aset == null)
            {
                return Json("Asset not found");
            }

            // Apply the updated values from asetParam to the existing asset
            aset.Aset_No = asetParam.Aset_No;
            aset.Line_No = asetParam.Line_No;   
            aset.Nomor_Aset_MySAP = asetParam.Nomor_Aset_MySAP;
            aset.MySAP_Line_No = asetParam.MySAP_Line_No;
            aset.Harmoni_No = asetParam.Harmoni_No;
            aset.Sinas_No = asetParam.Sinas_No;
            aset.Deskripsi = asetParam.Deskripsi;
            aset.Merk = asetParam.Merk;
            aset.Kategori_Asset = asetParam.Kategori_Asset;
            aset.Tahun_Perolehan = asetParam.Tahun_Perolehan;
            aset.Amount = asetParam.Amount;
            aset.Status_Penggunaan = asetParam.Status_Penggunaan;
            aset.Level = asetParam.Level;
            aset.Kondis_Aset = asetParam.Kondis_Aset;
            aset.Lokasi_Aset = asetParam.Lokasi_Aset;
            aset.Path_Foto_Tagging = asetParam.Path_Foto_Tagging;
            aset.Path_Foto_Keseluruhan = asetParam.Path_Foto_Keseluruhan;

            // Update the entity in the context
            _context.Aset.Update(aset);

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(asetParam.Aset_No))
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

            var aset = await _context.Aset.FirstOrDefaultAsync(m => m.Aset_No == Aset_No);
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

            var aset = await _context.Aset.FindAsync(Aset_No);
            if (aset == null)
            {
                return Json("Asset not found");
            }

            _context.Aset.Remove(aset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AssetExists(string id)
        {
            return _context.Aset.Any(e => e.Aset_No == id);
        }
    }
}

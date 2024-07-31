using System;
using System.Data.Entity;
using System.Linq;
using Kataandi.Models;
using Kataandi.Models.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZXing.QrCode.Internal;

namespace Kataandi.Controllers
{
    public class BarcodeScannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarcodeScannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var scannedData = _context.Barcodes.FromSqlRaw("SELECT * FROM master.dbo.[Tbl.barcode];").ToList();
            return View(scannedData);
        }

        [HttpPost]
        public ActionResult Scan(BarcodeChangeRequest request)
        {
            try
            {
                if (!string.IsNullOrEmpty(request.Id_sinas))
                {
                    var dataFilter = _context.Barcodes.FromSqlRaw($"SELECT * FROM master.dbo.[Tbl.barcode] WHERE Id_Sinas = {request.Id_sinas};");
                    return Json(dataFilter);
                }
                return Json("ini pasti null" + request.Id_sinas);
            } catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
using Kataandi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZXing;
using static System.Net.Mime.MediaTypeNames;
using ZXing.QrCode;
using SkiaSharp;
using ZXing.SkiaSharp;

namespace Kataandi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _applicationDbContext = applicationDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("test")]
        public IActionResult Test()
        {
            var image = new SKBitmap();
            BarcodeReader reader = new BarcodeReader();
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images.png");
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                image = SKBitmap.Decode(stream);
            }

            var result = reader.Decode(image);

            if (result != null)
            {
                // Get the decoded barcode text
                var barcodeText = result.Text;
                // Return the decoded barcode text as a JSON response
                return Json(new { barcodeText });
            }
            else
            {
                // Return a 404 response if the barcode could not be decoded
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Index1()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(double latitude, double longitude)
        {
            // Handle the received latitude and longitude
            // For demonstration, just passing them back to the view
            ViewBag.Latitude = latitude;
            ViewBag.Longitude = longitude;
            return View();
        }

        public async Task<IActionResult> TestDbConnection()
        {
            try
            {
                // Attempt a simple query to test the connection
                var canConnect = await _applicationDbContext.Database.CanConnectAsync();
                if (canConnect)
                {
                    return Content("Database connection successful.");
                }
                else
                {
                    return Content("Database connection failed.");
                }
            }
            catch (Exception ex)
            {
                return Content($"An error occurred: {ex.Message}");
            }
        }
    }
}


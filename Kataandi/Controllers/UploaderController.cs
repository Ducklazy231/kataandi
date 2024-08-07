using Kataandi.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;
using ZXing.SkiaSharp;

namespace Kataandi.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploaderController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        [Route("upload-file")]
        public IActionResult UploadFile([FromForm] IFormFile file)
        {
            try
            {
                FileModel fileModel = new FileModel();
                fileModel.file = file;

                using (var imageStream = new MemoryStream())
                {
                    file.CopyTo(imageStream);
                    imageStream.Seek(0, SeekOrigin.Begin); // Reset the stream position
                    var image = SKBitmap.Decode(imageStream);
                    if (image == null)
                    {
                        return NotFound("Unable to decode image.");
                    }

                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode(image);

                    Response.Headers.Add("X-Example-Header", "value");

                    if (result != null)
                    {
                        // Get the decoded barcode text
                        var barcodeText = result.Text;
                        // Return the decoded barcode text as a JSON response
                        return Ok(new { barcodeText });
                    }
                    else
                    {
                        // Return a 404 response if the barcode could not be decoded
                        return NotFound("Barcode could not be decoded.");
                    }
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
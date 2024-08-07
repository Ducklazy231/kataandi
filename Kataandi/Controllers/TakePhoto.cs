using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

public class TakePhoto : Controller
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public TakePhoto(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public ActionResult Index()
    {
        // Define the directory to store images
        string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        // Ensure the directory exists
        Directory.CreateDirectory(imageDirectory);

        // Get all image files from the directory
        string[] allImageFiles = Directory.GetFiles(imageDirectory, "*.txt");

        if (allImageFiles.Length > 0)
        {
            List<string> base64text = new List<string>();
            foreach (var item in allImageFiles)
            {
                base64text.Add(System.IO.File.ReadAllText(item));
            }
            ViewBag.Images = base64text;
        }

        return View();
    }

    [HttpPost]
    public void SaveImage(string base64image)
    {
        string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images");

        Directory.CreateDirectory(imageDirectory);

        string fileName = DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";

        string filePath = Path.Combine(imageDirectory, fileName);

        System.IO.File.WriteAllText(filePath, base64image);
    }
}

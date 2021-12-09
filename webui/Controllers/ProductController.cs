using System.Collections.Generic;
using Hotel_Booking.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.webui.Controllers
{
    public class ProductController:Controller
    {
         public IActionResult Index()
        {
            // ViewBag
            // ViewBag.Product ile ataması View'e yapılır
            
            // Model
            
            // ViewData
            // ViewData["Product"] = product ile ataması yapılır.
            return View();
        }
        // localhost:5000/product/list
        public IActionResult list()
        {
            return View();
        }
        // localhost:5000/product/details/2
        public IActionResult Details()
        {
            // ViewBag.Name = "Hotel X";
            // ViewBag.Price = 3000;
            // ViewBag.Description = "5 Star Hotel";

            return View();
        }
    }
}
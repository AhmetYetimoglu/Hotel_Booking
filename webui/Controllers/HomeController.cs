using System;
using System.Collections.Generic;
using Hotel_Booking.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {
        // localhost:5000/index
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
        // localhost:5000/home/details/2
        public IActionResult Details()
        {
            return View();
        }
    }
}
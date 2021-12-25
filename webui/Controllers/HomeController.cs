using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hotel_Booking.webui.Data;
using Hotel_Booking.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Booking.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {
        // localhost:5000/index
        [HttpGet]
         public IActionResult Index()
        {
            List<SelectListItem> rooms = new List<SelectListItem>();
            rooms.Add(new SelectListItem() {Text="Economy", Value = "Economy"});
            rooms.Add(new SelectListItem() {Text="Business", Value = "Business"});
            ViewBag.Rooms = rooms;
            List<SelectListItem> peoples = new List<SelectListItem>();
            peoples.Add(new SelectListItem() {Text="1", Value = "1"});
            peoples.Add(new SelectListItem() {Text="2", Value = "2"});
            peoples.Add(new SelectListItem() {Text="3", Value = "3"});
            peoples.Add(new SelectListItem() {Text="4", Value = "4"});
            peoples.Add(new SelectListItem() {Text="5", Value = "5"});
            peoples.Add(new SelectListItem() {Text="6", Value = "6"});
            ViewBag.Peoples = peoples;
            List<SelectListItem> children = new List<SelectListItem>();
            children.Add(new SelectListItem() {Text="1", Value = "1"});
            children.Add(new SelectListItem() {Text="2", Value = "2"});
            children.Add(new SelectListItem() {Text="3", Value = "3"});
            children.Add(new SelectListItem() {Text="4", Value = "4"});
            children.Add(new SelectListItem() {Text="5", Value = "5"});
            children.Add(new SelectListItem() {Text="6", Value = "6"});
            ViewBag.Children = children;
            // ViewBag
            // ViewBag.Product ile ataması View'e yapılır
            
            // Model
            
            // ViewData
            // ViewData["Product"] = product ile ataması yapılır.
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult Index(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product obj = new Product(){Name = model.Products[0].Name, ArrivalDate = model.Products[0].ArrivalDate, DepartureDate = model.Products[0].DepartureDate, Room = model.Products[0].Room, NumberOfPeople = model.Products[0].NumberOfPeople, NumberOfChildren = model.Products[0].NumberOfChildren};
                return RedirectToAction("list","Product", obj);
            }
            return View();
        }
    }
}
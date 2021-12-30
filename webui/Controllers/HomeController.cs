using System;
using System.Collections.Generic;
using business.Abstract;
using entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using webui.Models;

namespace Hotel_Booking.webui.Controllers
{
    // localhost:5000/home
    public class HomeController:Controller
    {
        private IProductService _productService;
        
        public HomeController(IProductService productService)
        {
            this._productService = productService;
        }
        // localhost:5000/index
        [HttpGet]
         public IActionResult Index()
        {
            List<SelectListItem> peoples = new List<SelectListItem>();
            peoples.Add(new SelectListItem() {Text="1", Value = "1"});
            peoples.Add(new SelectListItem() {Text="2", Value = "2"});
            peoples.Add(new SelectListItem() {Text="3", Value = "3"});
            peoples.Add(new SelectListItem() {Text="4", Value = "4"});
            peoples.Add(new SelectListItem() {Text="5", Value = "5"});
            peoples.Add(new SelectListItem() {Text="6", Value = "6"});
            ViewBag.Peoples = peoples;
            List<SelectListItem> children = new List<SelectListItem>();
            children.Add(new SelectListItem() {Text="0", Value = "0"});
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
         public IActionResult Index(ProductViewModel model)
        {
                Product obj = new Product(){Name = model.Products[0].Name, ArrivalDate = model.Products[0].ArrivalDate, DepartureDate = model.Products[0].DepartureDate, NumberOfPeople = model.Products[0].NumberOfPeople, NumberOfChildren = model.Products[0].NumberOfChildren};
                if(_productService.HomePageValidation(obj))
                {
                    return RedirectToAction("list","Shop", obj);    
                }
                CreateMessage(_productService.ErrorMessage,"danger");
                return View();
        }

        private void CreateMessage(List<string> message,string alerttype)
        {
            var msg = new AlertMessage()
            {
            Message = message,
            AlertType = alerttype
            };
            
            TempData["message"] = JsonConvert.SerializeObject(msg);
        }
    }
}
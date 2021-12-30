using System;
using System.Collections.Generic;
using business.Abstract;
using entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Booking.webui.Controllers
{
    public class ShopController:Controller
    {
        private IProductService _productService;
        
        public ShopController(IProductService productService)
        {
            this._productService = productService;
        }
        // localhost:5000/index
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

            
            var productViewModel = new ProductViewModel()
            {
                Products = _productService.GetAll()
            };

            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product obj = new Product(){Name = model.Products[0].Name, ArrivalDate = model.Products[0].ArrivalDate, DepartureDate = model.Products[0].DepartureDate, NumberOfPeople = model.Products[0].NumberOfPeople, NumberOfChildren = model.Products[0].NumberOfChildren};
                return RedirectToAction("list","Shop", obj);
            }
            return View();
        }
        // localhost:5000/product/list
        [HttpGet]
        public IActionResult list(Product model)
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

            List<Product> _products = new List<Product>();

            var productViewModel = new ProductViewModel()
            {
                Products = _productService.GetProductCity(model.Name)
            };

            return View(productViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
         public IActionResult list(ProductViewModel model)
        {
            if(ModelState.IsValid)
            {
                Product obj = new Product(){Name = model.Products[0].Name, ArrivalDate = model.Products[0].ArrivalDate, DepartureDate = model.Products[0].DepartureDate, Room = model.Products[0].Room, NumberOfPeople = model.Products[0].NumberOfPeople, NumberOfChildren = model.Products[0].NumberOfChildren};
                return RedirectToAction("list","Shop", obj);
            }
            return View();
        }
        // localhost:5000/home/details/2
        public IActionResult Details(string url)
        {
            if (url==null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails(url);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
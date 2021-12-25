using System;
using System.Collections.Generic;
using Hotel_Booking.webui.Data;
using Hotel_Booking.webui.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hotel_Booking.webui.Controllers
{
    public class ProductController:Controller
    {
        // localhost:5000/index
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

            // Console.WriteLine(model.Name);
            // Console.WriteLine(model.ArrivalDate);
            // Console.WriteLine(model.DepartureDate);
            // Console.WriteLine(model.Room);
            // Console.WriteLine(model.NumberOfPeople);
            // Console.WriteLine(model.NumberOfChildren);
            List<Product> _products = new List<Product>();
            _products = ProductRepository.Products;
            var productViewModel = new ProductViewModel()
            {
                Products = _products
            };

            return View(productViewModel);
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
        // localhost:5000/product/list
        [HttpGet]
        public IActionResult list(Product model)
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

            List<Product> _products = new List<Product>();

            for (int i = 0; i < ProductRepository.Products.Count; i++)
            {
                if(model.Name == ProductRepository.Products[i].Name)
                {
                    _products.Add(ProductRepository.Products[i]);
                }
                if(model.Name == ProductRepository.Products[i].City)
                {
                    _products.Add(ProductRepository.Products[i]);
                }
            }
            var productViewModel = new ProductViewModel()
            {
                Products = _products
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
                return RedirectToAction("list","Product", obj);
            }
            return View();
        }
        // localhost:5000/home/details/2
        public IActionResult Details(int id)
        {
            return View(ProductRepository.GetProductById(id));
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Product());
        }
        [HttpPost]
        public IActionResult Create(Product p)
        {
            if (ModelState.IsValid)
            {
                ProductRepository.AddProduct(p);
                return RedirectToAction("Index");
            }
            return View(p);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(ProductRepository.GetProductById(id));
        }
        [HttpPost]
        public IActionResult Edit(Product p)
        {
            ProductRepository.EditProduct(p);
            return RedirectToAction("Index");
        }
    }
}
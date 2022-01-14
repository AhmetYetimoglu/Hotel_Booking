using System;
using System.Collections.Generic;
using System.Globalization;
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
            for (int i = 0; i < productViewModel.Products.Count; i++)
            {
                if(productViewModel.Products[i].IsApproved == false)
                {
                    productViewModel.Products.Remove(productViewModel.Products[i]);
                }
            }

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
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime ArrivalDateTime = DateTime.ParseExact(model.ArrivalDate, "MM/dd/yyyy", provider);
            DateTime DepartureDateTime = DateTime.ParseExact(model.DepartureDate, "MM/dd/yyyy", provider);
            var TotalDays = DepartureDateTime.Day - ArrivalDateTime.Day;
            
            List<Product> _products = new List<Product>();
            var productViewModel = new ProductViewModel()
            {
                Products = _productService.GetProductCity(model.Name)
            };
            
            foreach (var product in productViewModel.Products)
            {
                product.AdultPrice = ((model.NumberOfPeople*product.AdultPrice) + (model.NumberOfChildren*product.ChildPrice))*TotalDays;
            }

            for (int i = 0; i < productViewModel.Products.Count; i++)
            {
                if((productViewModel.Products[i].ArrivalDate !=null) && (productViewModel.Products[i].DepartureDate !=null))
                {
                    
                    if (ArrivalDateTime>=(DateTime.ParseExact(productViewModel.Products[i].ArrivalDate, "MM/dd/yyyy", provider)) && 
                    (ArrivalDateTime<=DateTime.ParseExact(productViewModel.Products[i].DepartureDate, "MM/dd/yyyy", provider)) || 
                    (DepartureDateTime>=(DateTime.ParseExact(productViewModel.Products[i].ArrivalDate, "MM/dd/yyyy", provider)) && 
                    (DepartureDateTime<=(DateTime.ParseExact(productViewModel.Products[i].DepartureDate, "MM/dd/yyyy", provider)))))
                    {
                        productViewModel.Products.Remove(productViewModel.Products[i]);
                    }
                }
            }
            
            return View(productViewModel);
        }
    }
}
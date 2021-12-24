using System.Collections.Generic;
using Hotel_Booking.webui.Data;
using Hotel_Booking.webui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.webui.Controllers
{
    public class ProductController:Controller
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
        public IActionResult list(Reservation model)
        {
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
        // [HttpPost]
        // public IActionResult list(Product model) 
        // {
        //     var productViewModel = new ProductViewModel();

        //     for(int i = 0; i<products.Count; i++)
        //     {
        //         if(model.Name == products[i].Name)
        //         {
        //             obj[i] = products[i];
        //             obj[i].ArrivalDate = model.ArrivalDate;
        //             obj[i].DepartureDate = model.DepartureDate;
        //             obj[i].NumberOfPeople = model.NumberOfPeople;
        //             obj[i].NumberOfChildren = model.NumberOfChildren;
        //             obj[i].Room = model.Room;
        //         }
        //         if(model.City == products[i].City)
        //         {
        //             obj[i] = products[i];
        //             obj[i].ArrivalDate = model.ArrivalDate;
        //             obj[i].DepartureDate = model.DepartureDate;
        //             obj[i].NumberOfPeople = model.NumberOfPeople;
        //             obj[i].NumberOfChildren = model.NumberOfChildren;
        //             obj[i].Room = model.Room;
        //         }
        //         productViewModel.Products = obj;
        //     }
        //     return RedirectToAction("list");
        // }
        // localhost:5000/home/details/2
        public IActionResult Details(int id)
        {
            return View(ProductRepository.GetProductById(id));
        }
    }
}
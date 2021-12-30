using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using business.Abstract;
using entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webui.Models;

namespace webui.Controllers
{
    public class AdminController: Controller
    {
        private IProductService _productService;
        public AdminController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult ProductList()
        {
            return View(new ProductViewModel{
                Products = _productService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                City = model.City,
                ArrivalDate = model.ArrivalDate,
                DepartureDate = model.DepartureDate,
                Url = model.Url,
                AdultPrice = model.AdultPrice,
                ChildPrice = model.ChildPrice,
                ImageUrl = model.ImageUrl
            };
            
            if(_productService.Create(entity))
            {
                List<String> mesaj = new List<string>();
                mesaj.Add("kayit eklendi");
                CreateMessage(mesaj,"success");

                return RedirectToAction("ProductList");
            }
            CreateMessage(_productService.ErrorMessage,"danger");
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if(entity==null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                City = entity.City,
                ArrivalDate = entity.ArrivalDate,
                DepartureDate = entity.DepartureDate,
                Url = entity.Url,
                AdultPrice = entity.AdultPrice,
                ChildPrice = entity.ChildPrice,
                ImageUrl = entity.ImageUrl,
                IsApproved = entity.IsApproved
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductModel model,IFormFile file)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity ==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.City = model.City;
            entity.ArrivalDate = model.ArrivalDate;
            entity.DepartureDate = model.DepartureDate;
            entity.Url = model.Url;
            entity.AdultPrice = model.AdultPrice;
            entity.ChildPrice = model.ChildPrice;
            entity.IsApproved = model.IsApproved;
            //resim upload
            if(file!=null)
            {
                var exntention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}.{exntention}");
                entity.ImageUrl = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\images",randomName);

                using(var stream = new FileStream(path,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            
            _productService.Update(entity);
            List<String> mesaj = new List<string>();
            mesaj.Add(entity.Name+" isimli ürün güncellendi.");
            CreateMessage(mesaj,"success");
            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int productId)
        {
            Console.WriteLine(productId);
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            List<String> mesaj = new List<string>();
            mesaj.Add(entity.Name+" isimli ürün silindi.");
            CreateMessage(mesaj,"danger");
            return RedirectToAction("ProductList");
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
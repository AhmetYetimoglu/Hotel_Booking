using System;
using business.Abstract;
using entity;
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
                Url = model.Url,
                Price = model.Price,
                ImageUrl = model.ImageUrl
            };
            
            if(_productService.Create(entity))
            {
                CreateMessage("kayit eklendi","susccess");

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
                Url = entity.Url,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ProductModel model)
        {
            var entity = _productService.GetById(model.ProductId);
            if (entity ==null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.Url = model.Url;
            entity.Price = model.Price;
            entity.ImageUrl = model.ImageUrl;
            
            _productService.Update(entity);

            var msg = new AlertMessage()
            {
               Message = $"{entity.Name} isimli ürün güncellendi.",
               AlertType = "success"
            };
            
            TempData["message"] = JsonConvert.SerializeObject(msg);

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
            var msg = new AlertMessage()
            {
               Message = $"{entity.Name} isimli ürün silindi.",
               AlertType = "danger"
            };
            
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }
        private void CreateMessage(string message,string alerttype)
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
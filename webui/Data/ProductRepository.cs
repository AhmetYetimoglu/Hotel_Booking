using System.Collections.Generic;
using System.Linq;
using Hotel_Booking.webui.Models;

namespace Hotel_Booking.webui.Data
{
    class ProductRepository
    {
        private static List<Product> _products = null;

        static ProductRepository()
        {
            _products = new List<Product>
            {
                new Product {Name="Deneme 1", City="Antalya", Price=3000,IsApproved=true, ImageUrl= "hotel_1.jpg", ProductId =1},
                new Product {Name="Deneme 2", City="İzmir",Price=4000,IsApproved=true, ImageUrl= "hotel_2.jpg", ProductId =2},
                new Product {Name="Deneme 3", City="Antalya",Price=5000,IsApproved=true, ImageUrl= "hotel_3.jpg", ProductId =3},
                new Product {Name="Deneme 4", City="İzmir",Price=7000,IsApproved=true, ImageUrl= "hotel_4.jpg", ProductId =4},
                new Product {Name="Deneme 5", City="Antalya",Price=7000,IsApproved=true, ImageUrl= "hotel_5.jpg", ProductId =5},
            };
        }
        public static List<Product> Products{
            get{
                return _products;
            }
        }
        public static void AddProduct(Product product)
        {
            _products.Add(product);
        }
        public static Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p=>p.ProductId==id);
        }
        public static void EditProduct(Product product)
        {
            foreach (var p in _products)
            {
                if(p.ProductId == product.ProductId)
                {
                    p.Name = product.Name;
                    p.City = product.City;
                    p.Price = product.Price;
                    p.ImageUrl = product.ImageUrl;
                    p.IsApproved = true;
                    p.ReservedDate = product.ReservedDate;
                }
            }
        }
        public static void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if(product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
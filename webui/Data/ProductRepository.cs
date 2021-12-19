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
                new Product {ProductId=1, Name="Hotel2",Price=3000,Description="iyi", IsApproved=false},
                new Product {ProductId=2, Name="Hotel3",Price=4000,Description="çok iyi", IsApproved=true},
                new Product {ProductId=3, Name="Hotel4",Price=5000,Description="harika", IsApproved=true},
                new Product {ProductId=4, Name="Hotel5",Price=6000,Description="mükemmel", IsApproved=true},
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
    }
}
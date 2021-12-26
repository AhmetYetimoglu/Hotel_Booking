using entity;
using data.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace data.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, ShopContext>, IProductRepository
    {
        //Generic metotdan zaten bütün metotları alır ancak IProductRepository Interface'inden kendi kullanacağı metotların imzasını alır.
        public List<Product> GetRatingProducts()
        {
            using(var context = new ShopContext())
            {
                return context.Products.ToList();
            }
        }
    }
    // public class EfCoreProductRepository : IProductRepository
    // {
    //     //Database'e ulaşım için db nesnesi ürettik.
    //     private ShopContext db = new ShopContext();
    //     public void Create(Product entity)
    //     {
    //         db.Products.Add(entity);
    //         db.SaveChanges();
    //     }

    //     public void Delete(int id)
    //     {
    //         throw new System.NotImplementedException();
    //     }

    //     public List<Product> GetAll()
    //     {
    //         throw new System.NotImplementedException();
    //     }

    //     public Product GetById(int id)
    //     {
    //         throw new System.NotImplementedException();
    //     }

    //     public List<Product> GetRatingProducts()
    //     {
    //         throw new System.NotImplementedException();
    //     }

    //     public void Update(Product entity)
    //     {
    //         throw new System.NotImplementedException();
    //     }
    // }
}
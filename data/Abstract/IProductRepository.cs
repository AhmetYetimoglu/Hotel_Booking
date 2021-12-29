using System.Collections.Generic;
using entity;
namespace data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        Product GetProductDetails(string url);

        List<Product> GetProductCity(string city);
        List<Product> GetRatingProducts();
    }
}
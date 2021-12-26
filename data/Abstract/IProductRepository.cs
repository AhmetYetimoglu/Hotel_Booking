using System.Collections.Generic;
using entity;
namespace data.Abstract
{
    public interface IProductRepository:IRepository<Product>
    {
        List<Product> GetRatingProducts();
    }
}
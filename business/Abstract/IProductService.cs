using System.Collections.Generic;
using entity;

namespace business.Abstract
{
    public interface IProductService: IValidator<Product>
    {
        Product GetById(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductCity(string city);
        List<Product> GetAll();
        bool Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
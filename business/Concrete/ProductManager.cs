using System.Collections.Generic;
using business.Abstract;
using data.Abstract;
using entity;

namespace business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductRepository _productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool Create(Product entity)
        {
            // iş kuralları uygula
            if (Validation(entity))
            {
                _productRepository.Create(entity);
                return true;
            }
            return false;
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public Product GetById(int id)
        {
            return _productRepository.GetById(id);
        }
        public List<Product> GetProductCity(string city)
        {
            return _productRepository.GetProductCity(city);
        }

        public Product GetProductDetails(string url)
        {
            return _productRepository.GetProductDetails(url);
        }
        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }

        public List<string> ErrorMessage { get; set;}

        public bool Validation(Product entity)
        {
            ErrorMessage = new List<string>();
            var IsValid = true;
            if (string.IsNullOrEmpty(entity.Name))
            {
                ErrorMessage.Add("Hotel ismi girmelisiniz.");
                IsValid=false;
            }
            // if (string.IsNullOrEmpty(entity.City))
            // {
            //     ErrorMessage += "Hotelin bulunduğu şehri girmelisiniz.\n";
            // }
            if(entity.Price<=0)
            {
                ErrorMessage.Add("Ürünün fiyati negatif veya 0 olamaz.");
                IsValid=false;
            }

            return IsValid;
        }
    }
}
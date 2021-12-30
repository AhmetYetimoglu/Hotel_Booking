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
            if (string.IsNullOrEmpty(entity.City))
            {
                ErrorMessage.Add("Hotelin bulunduğu şehri girmelisiniz.");
            }
            if(entity.AdultPrice<=0)
            {
                ErrorMessage.Add("AdultPrice negatif veya 0 olamaz.");
                IsValid=false;
            }

            return IsValid;
        }
        

        public bool HomePageControl(Product entity)
        {
            // iş kuralları uygula
            if (HomePageValidation(entity))
            {
                return true;
            }
            return false;
        }
        
        public bool HomePageValidation(Product entity)
        {
            ErrorMessage = new List<string>();
            var IsValid = true;
            if ((string.IsNullOrEmpty(entity.Name))||(string.IsNullOrEmpty(entity.City)))
            {
                ErrorMessage.Add("Hotel ya da şehir ismi girmelisiniz.");
                IsValid=false;
            }
            if (string.IsNullOrEmpty(entity.ArrivalDate))
            {
                ErrorMessage.Add("Check-in boş olamaz.");
                IsValid=false;
            }
            if (string.IsNullOrEmpty(entity.DepartureDate))
            {
                ErrorMessage.Add("Check-out boş olamaz.");
                IsValid=false;
            }
            if((entity.NumberOfPeople+entity.NumberOfChildren) == 0)
            {
                ErrorMessage.Add("Lütfen en az bir çocuk ya da yetişkin girin.");
                IsValid=false;
            }

            return IsValid;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webui.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string City { get; set; }
        // public string? Url { get; set; }
        public double? ChildPrice {get; set; }
        public double? AdultPrice { get; set; }
        public bool IsApproved { get; set; }
        public string ImageUrl { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string Room { get; set; }
        public int? NumberOfPeople { get; set; }
        public int? NumberOfChildren { get; set; }
    }
}
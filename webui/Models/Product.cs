using System;
using System.Globalization;

namespace Hotel_Booking.webui.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public bool IsApproved { get; set; }
        public string ImageUrl { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfChildren { get; set; }
        public DateTime ArrivalDateTime => DateTime.ParseExact($"{ArrivalDate}", "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"));
        public DateTime DepartureDateTime => DateTime.ParseExact($"{DepartureDate}", "MM/dd/yyyy", CultureInfo.GetCultureInfo("en-US"));
    }
}
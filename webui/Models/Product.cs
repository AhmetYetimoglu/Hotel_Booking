using System;

namespace Hotel_Booking.webui.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }

        public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");
        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");
        public string Description { get; set; }
    }
}
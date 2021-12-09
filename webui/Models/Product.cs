using System;

namespace Hotel_Booking.webui.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }

        public string ArrivalDate => ArrivalDateTime.ToString("MM/dd/yyyy");
        public string DepartureDate => DepartureDateTime.ToString("MM/dd/yyyy");
        public double Price { get; set; }

        public string Description { get; set; }
    }
}
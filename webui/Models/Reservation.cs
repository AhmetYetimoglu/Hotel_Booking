namespace Hotel_Booking.webui.Models
{
    public class Reservation
    {
        public string Name { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }
        public string Room { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfChildren { get; set; }
    }
}
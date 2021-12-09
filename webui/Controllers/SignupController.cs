using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.webui.Controllers
{
    public class Signup:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
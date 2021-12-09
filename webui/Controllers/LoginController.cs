using Microsoft.AspNetCore.Mvc;

namespace Hotel_Booking.webui.Controllers
{
    public class LoginController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
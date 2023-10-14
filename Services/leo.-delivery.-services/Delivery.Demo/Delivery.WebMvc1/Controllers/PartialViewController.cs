using Microsoft.AspNetCore.Mvc;

namespace Delivery.WebMvc1.Controllers
{
    public class PartialViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

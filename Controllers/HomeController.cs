using Microsoft.AspNetCore.Mvc;

namespace system_petshop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

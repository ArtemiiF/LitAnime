using Microsoft.AspNetCore.Mvc;

namespace LitAnime.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

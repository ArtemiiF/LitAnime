using Microsoft.AspNetCore.Mvc;

namespace LitAnime.Controllers
{
    public class RatingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

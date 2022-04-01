using Microsoft.AspNetCore.Mvc;
using LitAnime.Models;
using Microsoft.AspNetCore.Authorization;

namespace LitAnime.Controllers
{
    public class HomeController : Controller
    {

        [AllowAnonymous]
        public async Task<IActionResult> Index(HomeViewModel model)
        {
            if (model.SearchString is null || model.SearchString == "")
            {
                return View(model);
            }

            return RedirectToAction("Index", "Search", new SearchViewModel() { q = model.SearchString });
        }
    }
}

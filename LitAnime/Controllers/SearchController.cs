using LitAnime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LitAnime.Services.SearchStringToTags;
using LitAnime.Domain;
using Microsoft.AspNetCore.Mvc.Routing;
using LitAnime.Services;

namespace LitAnime.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext appDbContext;

        public SearchController(AppDbContext dbContext)
        {
            appDbContext = dbContext;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery]SearchViewModel model)
        {
            List<string> tags = StringTransformer.TransformToTags(model.q);

            model.Pictures = appDbContext.GetPicturesByTags(tags);

            foreach (var item in model.Pictures)
            {
                item.Link = item.Link.Replace(Config.ImagePath, "/DBImage/");
            }


            return View(model);
        }
    }
}

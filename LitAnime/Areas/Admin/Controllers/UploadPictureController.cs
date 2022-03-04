using LitAnime.Domain;
using LitAnime.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LitAnime.Areas.Admin.Controllers
{
    [Area("admin")]
    public class UploadPictureController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        public UploadPictureController(AppDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            appDbContext = dbContext;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(new UploadPictureViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(UploadPictureViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(User.Identity.Name);
                if (user is null)
                {
                    ModelState.AddModelError(nameof(UploadPictureViewModel), "User Error");
                    return View(model);
                }

                //appDbContext.UploadPicture(model.FormFile, model.PicName, user);
                Picture tempPic = new Picture(model.FormFile, model.PicName, user);
                appDbContext.Pictures.Add(tempPic);
                appDbContext.SaveChanges();

                appDbContext.UploadTags(model.Tags, tempPic.Link ?? "");
            }
            else
                ModelState.AddModelError(nameof(UploadPictureViewModel), "Form Error");

            return View(model);
        }
    }
}

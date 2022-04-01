using LitAnime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using LitAnime.Domain;

namespace LitAnime.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly AppDbContext appDbContext;
        
        public AccountController(UserManager<User> userMgr, SignInManager<User> signMgr, AppDbContext dbContext)
        {
            userManager = userMgr;
            signInManager = signMgr;
            appDbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnURL)
        {
            ViewBag.returnUrl = returnURL;
            return View(new LoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnURL)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if(result.Succeeded)
                    {
                        return Redirect(returnURL ?? "Home");
                    }
                }
                ModelState.AddModelError(nameof(LoginViewModel.UserName), "Неверный логин или пароль");
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Register(string? returnURL)
        {
            ViewBag.returnUrl = returnURL;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnURL)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    ModelState.AddModelError(nameof(RegisterViewModel.UserName), "This user already exist"); 
                }
                else
                {
                    if (returnURL == null || returnURL == "")
                        returnURL = "Home";
                    User currUser = new User(model.UserName, model.Email, model.Password);

                    appDbContext.Users.Add(currUser);
                    appDbContext.UserRoles.Add(new IdentityUserRole<string>() {UserId=currUser.Id.ToString(), RoleId = "3"});
                    appDbContext.SaveChanges();

                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(currUser, model.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (returnURL == null || returnURL == "")
                            returnURL = "Home";
                        return Redirect(returnURL ?? "/");
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}

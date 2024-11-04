using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace RecipeShare.Controllers {
    public class AccountController : Controller {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }
    }
}

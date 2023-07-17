using Demo.DAL.Models;
using Demo.PL.ViewsModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _UserManager;
		private readonly SignInManager<ApplicationUser> _SignInManager;


		public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
			_UserManager = userManager;
			_SignInManager = signInManager;
		}

        

		#region Register
		public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid) // Server Side Validation 
            {
                var User = new ApplicationUser()
                {
                    FName = model.FName,
                    LName = model.LName,    
                    UserName = model.Emial.Split('@')[0],
                    Email = model.Emial,
                    IsAgree = model.IsAgree,

                };

                var result = await _UserManager.CreateAsync(User, model.Password);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Login));

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
		}
        #endregion

        #region Login

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _UserManager.FindByEmailAsync( model.Email);
                if (user is not null )
                {
                    var flag = await _UserManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        var result = await _SignInManager.PasswordSignInAsync(user,model.Password , model.RememberMe , false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");

                    }
                    ModelState.AddModelError(string.Empty, "Passward is not Existed");

                }
                ModelState.AddModelError(string.Empty, "Email is not Existed");
                 
            }
            return View(model);
        }
        #endregion

    }
}

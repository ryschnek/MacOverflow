using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MacOverflow.ViewModels;

namespace MacOverflow.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var vm = new LoginViewModel();

            return View("Login", vm);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var vm = new RegisterViewModel();

            return View("Register", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var user = await _userManager.FindByNameAsync(vm.Username);

            if(user != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result;
                result = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", "Password does not match!");
                }
            }
            else
            {
                ModelState.AddModelError("Username", "Entered username is not found!");
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = vm.Username,
                };

                var result = await _userManager.CreateAsync(user, vm.Password);

                if (result.Succeeded)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, vm.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", $"{result.Errors}");
                }
            }
            return View("Register", vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction("Index", "Home");
        }
    }
}

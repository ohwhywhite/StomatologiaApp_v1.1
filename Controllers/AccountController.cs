using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stomatologia.Models;
using Stomatologia.ViewModels;

namespace Stomatologia.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                PESEL = model.PESEL,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, ApplicationRoles.User);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                ModelState.AddModelError(string.Empty, "Błąd Rejestracji konta");

                return View(model);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Nieprawidłowa próba logowania");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Nieprawidłowa próba logowania");
                return View(model);
            }

            return RedirectToAction("Profile", "Account");
        }

        [HttpGet]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> Profile()
        {
            var userEmail = User.Identity?.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);

            var model = new UserProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PESEL = user.PESEL,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = ApplicationRoles.User)]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            //How my user authentication works
            //var user = _userService.GetById(3);
            //user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, "bambang123");
            //user.SelectedUserRole = new List<int> { 2 };
            //_userService.Update(user);
            //var result = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, "abc12");
            //GetCurrentUser().

            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogIn(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return PartialView("_LogIn",model);
        }

        [HttpPost]
        public async Task<IActionResult> DoLogIn(LoginViewModel model)
        {
            var res = await _signInManager.PasswordSignInAsync(model.Username,
                model.Password, model.RememberMe, false);
            if (res.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return PartialView("_LogIn", model);
        }

        public IActionResult Register()
        {
            return null;
        }
    }
}
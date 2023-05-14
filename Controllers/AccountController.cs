using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
using ecommerce_website_simple.ViewModels.Auth;
namespace ecommerce_website_simple.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        public IActionResult SignIn(string returnUrl)
        {
            return View(new SignInViewModel{ ReturnUrl = returnUrl });
        }
        //Registering new user
        //Note: have not implemented AnitForgery
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser newUser = new()
            {
                Email = register.EmailAddress,
                UserName = register.FirstName,
                FirstName = register.FirstName,
                LastName = register.LastName,

            };
            var response = await _userManager.CreateAsync(newUser, register.Password);
            await _userManager.AddToRoleAsync(newUser, "User");
            if (!response.Succeeded)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError("Error creating user", item.Description);
                }
                return View(register);
            }
            return RedirectToAction("SignIn");
        }
        //logging in exsiting user
        //Note: have not implemented AnitForgery
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel signin)
        {
            if (!ModelState.IsValid) return View();
            ApplicationUser appUser = await _userManager.FindByEmailAsync(signin.EmailAddress); 
            if(appUser == null)
            {
                ModelState.AddModelError("", "No user with this email exists");
                return View(signin);
            }
            else
            {
                var passwordCheck = await _signInManager.PasswordSignInAsync(appUser, signin.Password, signin.RememberMe,true);
                if (!passwordCheck.Succeeded)
                {
                    ModelState.AddModelError("", "Invalid password or username.");
                    return View(signin);
                }
                else
                {
                    return Redirect(signin.ReturnUrl ?? "/");
                }
            }
            

        }

        public IActionResult Index()
        {
            return View();
        }
       //User signing out, not ADMIN signout
        public async Task<RedirectResult> SignOut(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}

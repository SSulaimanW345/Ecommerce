using ecommerce_website_simple.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ecommerce_website_simple.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext DBcontext, SignInManager<ApplicationUser> signInManager)
        {
            _dbcontext = DBcontext;
            _signInManager = signInManager;
        }
        //Controller returns the List of Users signed up and using the application
        public async Task<IActionResult> Index()
        {
            var users =_dbcontext.Users.ToList();
            return View(users);

        }

        //This controller returns LIST OF ORDERS
        public async Task<IActionResult>Orders()
        {
            var orders = await _dbcontext.Orders.ToListAsync();
            foreach(Order orderitem in orders)
            {
                _dbcontext.Entry(orderitem).Reference(x => x.Customer).Load();
                _dbcontext.Entry(orderitem).Reference(x => x.Address).Load();

            }
            return View(orders);
        }
        //This controller allows ONLY ADMIN to signout
        public async Task<RedirectResult> SignOut(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}

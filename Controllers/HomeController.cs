using ecommerce_website_simple.Models;
using ecommerce_website_simple.ViewModels.UserDashBoard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_website_simple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext DBcontext, UserManager<ApplicationUser> userManager)
        {
            _dbcontext = DBcontext;
            _userManager = userManager;
        }
        //Controller to display Different Index Pages According to User ROLE
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.User.IsInRole("User"))
            {
                return RedirectToAction("UserDashBoard", "Home");
            }
            else if (HttpContext.User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }
        
        //[Authorize(Roles = "Admin")]
        //public IActionResult Admin()
        //{
        //    return RedirectToAction("Index", "Home", new { area = "Admin" });

        //}
        //Displays logged in USERDASHBOARD -> This is the index page for USER role. 
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserDashBoard()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userOrders = await _dbcontext.Orders.Where(x => x.CustomerID == user.Id).ToListAsync();
            if (user == null)
                return NotFound();
            UserDashBoardVM userDashBoard = new UserDashBoardVM { AppUser=user,Orders=userOrders };
            return View(userDashBoard);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

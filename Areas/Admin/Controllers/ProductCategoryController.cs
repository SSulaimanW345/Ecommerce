using ecommerce_website_simple.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/ProductCategory")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public ProductCategoryController(ApplicationDbContext DBcontext)
        {
            _dbcontext = DBcontext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbcontext.ProductCategories.ToListAsync();
            return View(categories);
        }
      
    }
}

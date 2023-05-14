using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce_website_simple.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public ProductController(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        //Controller returns products based on QUERY STRING
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var products = from s in _dbcontext.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }

            return View(await products.AsNoTracking().ToListAsync());
        }
        //public IActionResult Edit(string? id)
        //{
        //    var productToupdate = _dbcontext.products.Where(x => x.productID == id).FirstOrDefault();
        //    if (productToupdate == null || id == null) return View("NotFound");
        //    return View(productToupdate);
        //}

        //public IActionResult Create()
        //{
        //    return View();
        //}

    }
}

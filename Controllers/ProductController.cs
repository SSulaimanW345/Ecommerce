using ecommerce_website_simple.Data;
using ecommerce_website_simple.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Controllers
{
    [Route("Product")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;
        
        public ProductController(ApplicationDbContext dbcontext)
        { 
            _dbcontext = dbcontext;
        }
        //Controller to DISPLAY PRODUCTS based on SearchString
        //Copied from DOCUMENTATION
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
        //Adds ITEMS to CART when user in on PRODUCT PAGE.
        [HttpPost]
        public async Task<IActionResult> AddToCart(string id)
        {
            Product product = await _dbcontext.Products.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.Product.ProductID == id).FirstOrDefault();

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    Product = product,
                    Quantity = 1,
                };
                cart.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.Set("Cart", cart);
            TempData["Success"] = "Product added to Cart";

            return RedirectToAction("Index", "Product");
        }

    }
}

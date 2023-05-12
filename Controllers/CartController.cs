using ecommerce_website_simple.Models;
using ecommerce_website_simple.ViewModels.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Data;
namespace ecommerce_website_simple.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public CartController(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public IActionResult Index()
        {
            List<CartItem> CartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            double subtotal = CartItems.Sum(x => x.Quantity * x.Product.ProductPrice);
            CartViewModel CartVM = new()
            {
                CartItems = CartItems,
                SubTotal = subtotal,
                Tax = 0.1,
                Total = (0.1 + 1) * subtotal,
            };

            return View(CartVM);
        }

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

            return RedirectToAction("Index","Cart");
        }

        public async Task<IActionResult>RemoveFromCart(string id)
        {
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.Product.ProductID == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.Product.ProductID == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Set("Cart", cart);
            }

            TempData["Success"] = "The product has been removed from your cart";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult>RemoveAll(string id)
        {
            List<CartItem> cart = HttpContext.Session.Get<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.Product.ProductID == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.Set("Cart", cart);
            }

            TempData["Success"] = "All products have been removed.Cart is empty";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}

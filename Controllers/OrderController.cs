using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
using ecommerce_website_simple.Data;
using ecommerce_website_simple.ViewModels.ShoppingCart;
using ecommerce_website_simple.ViewModels.OrderSummary;
namespace ecommerce_website_simple.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _dbcontext;

        public OrderController(ApplicationDbContext dbcontext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _dbcontext = dbcontext;
        }
        //Controller returns ORDERSUMMARYVM View as user will fill in order details . 
        public async Task<IActionResult>ProceedToCheckout()
        {
            
            List<CartItem> CartItems = HttpContext.Session.Get<List<CartItem>>("Cart") ?? new List<CartItem>();
            if (CartItems.Count == 0)
            {
                TempData["Error"] = "Please add a product to your Cart";
                return RedirectToAction("Index", "Cart");
            }; 
            double subtotal = CartItems.Sum(x => x.Quantity * x.Product.ProductPrice);
            OrderSummary OrderSummaryVM = new()
            {
                CartItems = CartItems,
                SubTotal = subtotal,
                Tax = 0.1,
                Total = (0.1 + 1) * subtotal,
            };
            return View(OrderSummaryVM);
        }
        //Controller to add confirm order, validate and add entry to DATABASE + empty CART.
        public async Task<IActionResult> PlaceOrder(OrderSummary orderVM)
        {
            List<CartItem> CartItems = HttpContext.Session.Get<List<CartItem>>("Cart");
            double subtotal = CartItems.Sum(x => x.Quantity * x.Product.ProductPrice);
            double Total = subtotal * (1.1);
            var address = new Address()
            {
                State = orderVM.Address.State,
                Country = orderVM.Address.Country,
                AddressDescription = orderVM.Address.AddressDescription,
            };
            await _dbcontext.AddAsync(address);
            await _dbcontext.SaveChangesAsync();
            Order order = new Order();

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                order.CustomerID = user.Id;
            }
            order.Fullname = orderVM.Fullname;
            order.Email = orderVM.Email;
            order.TotalCost = Total;
            order.AddressID =address.AddressID;
            await _dbcontext.Orders.AddAsync(order);
            await _dbcontext.SaveChangesAsync();
            foreach (CartItem item in CartItems)
            {
                OrderItem orderitem = new OrderItem
                {
                    OrderID = order.OrderID,
                    ProductID = item.Product.ProductID,
                };
                await _dbcontext.OrderItems.AddAsync(orderitem);
                await _dbcontext.SaveChangesAsync();
            }
            HttpContext.Session.Remove("Cart");
            await _dbcontext.SaveChangesAsync();
            orderVM.CartItems = CartItems;
            orderVM.Tax = 0.1;
            orderVM.SubTotal = subtotal;
            orderVM.Total = Total;
            return View(orderVM);
        }
    }
}

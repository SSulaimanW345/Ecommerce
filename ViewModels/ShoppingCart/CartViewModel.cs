using ecommerce_website_simple.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.ViewModels.ShoppingCart
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
    }
}

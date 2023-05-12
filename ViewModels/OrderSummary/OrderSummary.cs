using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
namespace ecommerce_website_simple.ViewModels.OrderSummary
{
    public class OrderSummary
    {
        public List<CartItem> CartItems { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; }
        public double Total { get; set; }
        [Required(ErrorMessage = "please enter your name")]
        public string Fullname { get; set; }
        [Required(ErrorMessage = "please enter your email")]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string PaymentType { get; set; }
        [Required(ErrorMessage = "please enter Address")]
        public Address Address { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_website_simple.Models;
namespace ecommerce_website_simple.ViewModels.UserDashBoard
{
    public class UserDashBoardVM
    {
        public ApplicationUser AppUser { get; set; }
        public List<Order> Orders { get; set; }
    }
}

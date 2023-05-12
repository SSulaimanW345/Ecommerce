using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required(ErrorMessage = "please enter your first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "please enter your last name")]
        public string LastName { get; set; }
        
        
    }
}

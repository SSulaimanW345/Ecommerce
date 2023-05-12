using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Models
{
    public class ProductCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Please enter a Category name"), MaxLength(50, ErrorMessage = "Cannot exceed more than 50 words")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Please enter a Category description"), MaxLength(200, ErrorMessage = "Cannot exceed more than 50 words")]
        public string CategoryDescription { get; set; }

    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string ProductID { get; set; }

        [Required(ErrorMessage = "Please enter a value"), MaxLength(50, ErrorMessage = "Cannot exceed more than 50 words")]
        public string ProductName { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Cannot exceed more than 200 words")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please choose a product category")]
        public ProductCategory Category { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Please enter a price for the product")]
        public double ProductPrice { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a quantity in inventory")]
        public int StockQuantity { get; set; }
        public string ProductImgURL { get; set; } = "default.png";
       

    }
}

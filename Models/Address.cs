using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Models
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string AddressID { get; set; }
        [Required(ErrorMessage = "Please enter a state description"), MaxLength(200, ErrorMessage = "Cannot exceed more than 50 words")]
        public string State { get; set; }
        [Required(ErrorMessage = "Please enter a country name"), MaxLength(50, ErrorMessage = "Cannot exceed more than 50 words")]
        public string Country { get; set; }
        public string AddressDescription { get; set; }


    }
}

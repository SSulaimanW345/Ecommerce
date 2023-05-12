using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string OrderID { get; set; }
        public string CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual ApplicationUser Customer { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Invalid cost")]
        public double TotalCost { get; set; }

        public List<OrderItem> OrderItems = new List<OrderItem>();

        public string Fullname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get;set; }
        public string PaymentType { get; set; }

        public string AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        //payment details
        //payment method
        //time of order placement
        //status of order => delivered or undelivered
        //All of the above commented left out for simplicity.

    }
}

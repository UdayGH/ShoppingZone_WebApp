using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingZone.Models
{
    public class Cart
    {
        [Key] 
        public int proId { get; set; }
        public string proname { get; set; }
        public int qty { get; set; }
        public int price { get; set; }
        public int bill { get; set; }
    }
}
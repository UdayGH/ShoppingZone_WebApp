using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShoppingZone.Models
{
    public class UserLogin
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        [DataType(DataType.Password)]   
        public string Password { get; set; }
    }
}
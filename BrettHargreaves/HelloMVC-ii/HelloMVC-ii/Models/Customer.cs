using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HelloMVC_ii.Models
{
    public class Customer
    {
        public string Id { get; set; }
        //Decorators for enforcing Model Validaton
        [Required]
        [StringLength(10,ErrorMessage ="Your string is too long!!")]
        [DisplayName("Enter your name")]
        public string Name { get; set; }
        public string Telephone { get; set; }
    }
}
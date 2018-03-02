using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Onboard2.Models
{
    public class MyProduk
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please input product Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please input product price")]
        public decimal Price { get; set; }
        
    }
}
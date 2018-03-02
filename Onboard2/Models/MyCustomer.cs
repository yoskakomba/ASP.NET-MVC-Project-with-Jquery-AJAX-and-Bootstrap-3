using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Onboard2.Models
{
    public class MyCustomer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please input Customer's Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please input Customer's address")]
        public string Address { get; set; }
    }
}
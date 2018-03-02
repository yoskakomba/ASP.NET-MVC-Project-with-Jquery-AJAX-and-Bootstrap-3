using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Onboard2.Models
{
    public class MyStore
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please input store Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please input store Name")]
        public string Address { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Onboard2.Models
{
    public class MySale
    {
        public int Id { get; set; }
        
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Please select product Name")]
        [DisplayName("Product")]
        public string ProductName { get; set; }

        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Please input Customer's Name")]
        [DisplayName("Customer")]
        public string CustomerName { get; set; }

        public int StoreId { get; set; }
        [DisplayName("Store")]
        [Required(ErrorMessage = "Please input store Name")]
        public string StoreName { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "Please input date sold")]
        public DateTime? DateSold { get; set; }

      
       
    }
}
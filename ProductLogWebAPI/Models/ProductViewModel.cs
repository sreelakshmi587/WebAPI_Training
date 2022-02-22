using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProductLogWebAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
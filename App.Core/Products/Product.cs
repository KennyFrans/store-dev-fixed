using App.Commons;
using System;
using System.Collections.Generic;
using System.Text;
using App.Core.OrderDetails;

namespace App.Core.Products
{
    public class Product : EntityBase
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}

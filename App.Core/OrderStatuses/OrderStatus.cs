using System;
using System.Collections.Generic;
using System.Text;
using App.Commons;
using App.Core.Orders;

namespace App.Core.OrderStatuses
{
    public class OrderStatus:EntityBase
    {
        public string Descrition { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}

using App.Commons;
using App.Core.OrderStatuses;
using App.Core.Products;

namespace App.Core.Orders
{
    public class Order:EntityBase
    {
        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}

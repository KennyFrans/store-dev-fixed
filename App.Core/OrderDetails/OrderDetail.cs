using App.Commons;
using App.Core.Products;

namespace App.Core.OrderDetails
{
    public class OrderDetail:EntityBase
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

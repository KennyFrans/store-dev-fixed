using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CartViewModel
    {
        public string Code { get; set; }
        public string Desc { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; } = 0;
        public decimal Price { get; set; }
    }
}

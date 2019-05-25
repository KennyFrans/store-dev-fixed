using App.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Core.Products
{
    public interface IProductRepo : IRepository<Product>
    {
        IQueryable<Product> GetForPagination();
    }
}

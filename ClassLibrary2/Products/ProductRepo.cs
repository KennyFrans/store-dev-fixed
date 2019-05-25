using System.Linq;
using App.Core.Products;
using Microsoft.EntityFrameworkCore;

namespace App.Repo.Products
{
    public class ProductRepo:BaseRepo<Product>,IProductRepo
    {
        private readonly BaseContext _context = new BaseContext();
        public IQueryable<Product> GetForPagination()
        {
            //no using statement cause DI container take care if disposing context
            return _context.Products.AsNoTracking();
        }
    }
}

using App.Commons;
using System.Linq;

namespace App.Core.Products
{
    public interface IProductService
    {
        Product GetByCode(string code);
        IQueryable<Product> GetProductsForPagination();
    }

    public class ProductService : DomainServiceBase<Product>,IProductService
    {
        private readonly IProductRepo _productRepo;
        public ProductService(IProductRepo productRepo) : base(productRepo)
        {
            _productRepo = productRepo;
        }

        

        public override Product CreateNew()
        {
            return new Product { Code = GenerateCode(), };
        }

        public string GenerateCode()
        {
            return "PR" + (entityRepo.GetCount() + 1).ToString("0000#");
        }

        public Product GetByCode(string code)
        {
            return _productRepo.GetAll().FirstOrDefault(x => x.Code.ToLower() == code.ToLower());
        }

        public IQueryable<Product> GetProductsForPagination()
        {
            return _productRepo.GetForPagination();
        }
    }
}

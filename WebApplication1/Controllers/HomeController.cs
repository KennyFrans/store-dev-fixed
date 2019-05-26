using App.Core.Products;
using App.Core.Users;
using App.Repo.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ProductService _productService;


        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager) : base(userManager,
            signInManager)
        {
            _productService = new ProductService(new ProductRepo());
        }

        public async Task<IActionResult> Index(string value,string sortorder,int page = 1)
        {
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortorder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortorder == "Price" ? "price_desc" : "Price";

            var entity = _productService.GetProductsForPagination().Select(x => new HomeViewModel
            {
                Code = x.Code,
                Name = x.Name,
                Desc = x.Description,
                UnitPrice = x.UnitPrice
            }).OrderBy(x=>x.Code);
            if (!string.IsNullOrEmpty(value))
            {
                entity = entity.Where(x => x.Name.ToLower().Contains(value.ToLower())).OrderBy(x=>x.Code);
            }

            switch (sortorder)
            {
                case "name_desc":
                    entity = entity.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    entity = entity.OrderBy(s => s.UnitPrice);
                    break;
                case "price_desc":
                    entity = entity.OrderByDescending(s => s.UnitPrice);
                    break;
                default:
                    //entity = entity.OrderBy(s => s.Name);
                    break;
            }
            var model = await PagingList.CreateAsync(entity, 10, page);
            return View(model);
        }

        public IActionResult Detail(string code = "")
        {
            if (!string.IsNullOrEmpty(code))
            {
                var entity = _productService.GetByCode(code);
                if (entity != null)
                {
                    var vm = new DetailViewModel
                    {   
                        Code = entity.Code,
                        Name = entity.Name,
                        Desc = entity.Description,
                        UnitPrice = entity.UnitPrice
                    };
                    return PartialView("_Detail", vm);
                }
                return PartialView("_Detail", new DetailViewModel());

            }

            return PartialView("_Detail", new DetailViewModel());
        }

        [HttpPost]
        public IActionResult Add(string code = "")
        {
            var listCart = GetCartData();
            var entity = _productService.GetByCode(code);
            if (entity != null)
            {
               
                if (listCart.Any(x=>x.Code == code))
                {
                    var currentItem  = listCart.FirstOrDefault(e=>e.Code == code);
                    listCart.Remove(currentItem);
                    if (currentItem != null)
                    {
                        currentItem.Price += entity.UnitPrice;
                        currentItem.Qty += 1;
                    }
                    listCart.Add(currentItem);
                    SetCartData(listCart);
                }
                else
                {
                    var cart = new CartViewModel
                    {
                        Code = entity.Code,
                        Desc = entity.Description,
                        Name = entity.Name,
                        Price = entity.UnitPrice,
                    };
                    cart.Qty += 1;
                    listCart.Add(cart);
                    SetCartData(listCart);
                }
               

                return Json(
                    new
                    {
                        success = true,
                        responseText = "Debug"
                    }
                );
            }

            return Json(
                new
                {
                    success = false,
                    responseText = "Error"
                }
            );

        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<CartViewModel> GetCartData()
        {
            if (HttpContext.Session.GetObject<List<CartViewModel>>("cart") == null)
            {
                HttpContext.Session.SetObject("cart", new List<CartViewModel>());
            }

            return HttpContext.Session.GetObject<List<CartViewModel>>("cart");
           
        }

        private void SetCartData(List<CartViewModel> obj)
        {
            HttpContext.Session.SetObject("cart", obj);
        }

        private void RemoveCartData()
        {
            HttpContext.Session.Remove("cart");
        }

    }
}

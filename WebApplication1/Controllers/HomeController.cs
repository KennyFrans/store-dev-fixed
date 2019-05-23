using System.Collections.Generic;
using App.Core.Products;
using App.Repo.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using App.Core.Users;
using App.Identity;
using App.Repo.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public IActionResult Index(string value)
        {
            var entity = _productService.GetAll().Select(x=> new HomeViewModel
            {
                Code = x.Code,
                Name = x.Name,
                Desc = x.Description,
                UnitPrice = x.UnitPrice
            }).ToList();
            if (!string.IsNullOrEmpty(value))
            {
                entity = entity.Where(x => x.Name.ToLower().Contains(value.ToLower())).ToList();
            }
            return View(entity);
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

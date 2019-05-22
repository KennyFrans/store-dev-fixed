using System.Collections.Generic;
using App.Core.Products;
using App.Repo.Products;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Helper;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductService _productService;
        public CartController()
        {
            _productService = new ProductService(new ProductRepo());
        }
        public IActionResult Index()
        {
            var listCart = GetCartData();
            return PartialView("_Index",listCart);
        }

        public IActionResult DeleteItem(string code = "")
        {
            return Json(
                new
                {
                    success = true,
                    responseText = "Debug"
                }
            );
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
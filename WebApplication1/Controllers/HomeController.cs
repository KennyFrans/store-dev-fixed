using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App.Core.Products;
using App.Repo.Products;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        public HomeController()
        {
            _productService = new ProductService(new ProductRepo());
        }

        public IActionResult Index()
        {
            var entity = _productService.GetAll().Select(x=> new HomeViewModel
            {
                Code = x.Code,
                Name = x.Name,
                Desc = x.Description,
                UnitPrice = x.UnitPrice
            }).ToList();
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
                        UnitPrice = entity.UnitPrice
                    };
                    return View(vm);
                }
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        public IActionResult Add(string code = "")
        {
            return Json(
                new
                {
                    success = true,
                    responseText = "Item ditambah"
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
    }
}

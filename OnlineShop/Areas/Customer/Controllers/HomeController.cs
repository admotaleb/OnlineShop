using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;

namespace OnlineShop.Controllers
{
    [Area("Customer")]

    public class HomeController : Controller
    {
        private ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.productTypes).Include(c=>c.specialTags).ToList());
        }

        //Get product details
        public IActionResult Details(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            if (id==null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public IActionResult ProductDetails(int? id)
        {
            List<Products>products = new List<Products>();
            if (id == null)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).FirstOrDefault(c => c.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            products = HttpContext.Session.Get<List<Products>>("products");
            if (products == null)
            {
                products = new List<Products>();
            }
            products.Add(product);
            HttpContext.Session.Set("products", products);
            return View(product);
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
        [ActionName("Remove")]
        public IActionResult RemoveToProduct(int? id)
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Remove(int? id)
        {
          List<Products> products =  HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                var product = products.FirstOrDefault(c => c.Id == id);
                if (product != null)
                {
                    products.Remove(product);
                    HttpContext.Session.Set("products", products);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Card()
        {
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products==null)
            {
                products = new List<Products>();
            }
            return View(products);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHostingEnvironment _he;
        public ProductsController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.productTypes).Include(f=>f.specialTags).ToList());
        }
        [HttpPost]
        public IActionResult Index(decimal lowerNumber, decimal highNumber)
        {
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.specialTags)
                .Where(c => c.Price >=lowerNumber && c.Price <= highNumber).ToList();
            return View(product);
        }
        //Create Get Method

        public ActionResult Create()
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTags"] = new SelectList(_db.SpecialTags.ToList(), "Id", "TagName");
            return View();
        }

        //Create Post Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                var searchProductName = _db.Products.FirstOrDefault(c => c.Name == products.Name);
                if (searchProductName != null)
                {
                    ViewBag.Message = "This Product Name is already Exist";
                    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["SpecialTags"] = new SelectList(_db.SpecialTags.ToList(), "Id", "TagName");
                    
                    return View(products);
                }

                if (image != null)
                              {
                    var name = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "/Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "/Images/no-image.jpg";
                }
            _db.Products.Add(products);
            await _db.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }
            return View(products);
        }

        //Create Edit Action Method
        public ActionResult Edit(int? id)
        {
            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["SpecialTags"] = new SelectList(_db.SpecialTags.ToList(), "Id", "TagName");
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(c=>c.productTypes).Include(f=>f.specialTags).FirstOrDefault(c=>c.Id==id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Products products, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images/", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    products.Image = "/Images/" + image.FileName;
                }
                if (image == null)
                {
                    products.Image = "/Images/no-image.jpg";
                }
                _db.Products.Update(products);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(products);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(c => c.productTypes).Include(f => f.specialTags).FirstOrDefault(c => c.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var products = _db.Products.Include(c => c.productTypes).Include(f => f.specialTags).FirstOrDefault(c => c.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            return View(products);
        }

        //Create Post Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirme(int? id, Products products)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != products.Id)
            {
                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).Include(f => f.specialTags).FirstOrDefault(c => c.Id == id);

            if (!ModelState.IsValid)
            {
                _db.Remove(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(products);
        }



    }
}
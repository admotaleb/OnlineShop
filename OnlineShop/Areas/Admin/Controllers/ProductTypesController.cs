using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
       private ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }

        //Create Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Create Post Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {
            if (id==null)
            {
                return NotFound();
            }
            if (id != productTypes.Id)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (ModelState.IsValid)
            {
                _db.Remove(productType);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(productTypes);
        }

        //Create Edit Action Method
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }
 
        //Create Post Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(ProductTypes productTypes )
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(productTypes);
        }



        //Create Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        //Create Post Details Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Details(ProductTypes productTypes)
        {
            return RedirectToAction(actionName: nameof(Index));
        }

        //Create Get Action Method
        public ActionResult Create()
        {
            return View();
        }

        //Create Post Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();

                //TempData["save"] = "Data Saved ";
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(productTypes);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        

        private ApplicationDbContext _db;

        public SpecialTagsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.SpecialTags.ToList());
        }

        //Create Get Method

        public ActionResult Create()
        {
            return View();
        }

        //Create Post Method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Add(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            return View(specialTags);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }

        //Create Post Edit Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _db.SpecialTags.Update(specialTags);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(specialTags);
        }

        //Create Details Action Method
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }

        //Create Post Details Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Details(SpecialTags specialTags)
        {
            return RedirectToAction(actionName: nameof(Index));
        }


        //Create Delete Action Method
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = _db.SpecialTags.Find(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }

        //Create Post Delete Action Method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id, SpecialTags specialTags)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (id != specialTags.Id)
            {
                return NotFound();
            }
            var specialTag = _db.SpecialTags.Find(id);
            if (ModelState.IsValid)
            {
                _db.Remove(specialTag);
                await _db.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));

            }
            return View(specialTag);
        }

    }
}
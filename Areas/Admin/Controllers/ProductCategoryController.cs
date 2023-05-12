using ecommerce_website_simple.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_website_simple.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    [Route("Admin/ProductCategory")]
    public class ProductCategoryController : Controller
    {
        private readonly ApplicationDbContext _dbcontext;

        public ProductCategoryController(ApplicationDbContext DBcontext)
        {
            _dbcontext = DBcontext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbcontext.ProductCategories.ToListAsync();
            return View(categories);
        }
        //[HttpGet("{id:int}")]
        //public IActionResult Edit(int? id)
        //{
        //    var categoryToupdate = _dbcontext.productCategories.Where(x => x.categoryID == id).FirstOrDefault();
        //    if (categoryToupdate == null || id == null) return View("NotFound");
        //    return View(categoryToupdate);
        //}
        //[HttpGet, ActionName("Create")]
        //public IActionResult Create()
        //{   
        //    return View();
        //}


        //[HttpPost, ActionName("Edit")]
        //public async Task<IActionResult>Edit(ProductCategory category)
        //{
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    var categoryUpdate = await _dbcontext.productCategories.FirstOrDefaultAsync(s => s.categoryID == category.categoryID);
        //    if (await TryUpdateModelAsync<ProductCategory>(
        //        categoryUpdate,
        //        "",
        //        s => s.categoryName, s => s.categoryDescription))
        //    {
        //        try
        //        {
        //            await _dbcontext.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (DbUpdateException /* ex */)
        //        {
        //            //Log the error (uncomment ex variable name and write a log.)
        //            ModelState.AddModelError("", "Unable to save changes. " +
        //                "Try again, and if the problem persists, " +
        //                "see your system administrator.");
        //        }
        //    }
        //    return View(categoryUpdate);
        //}
        
        
        //[HttpPost, ActionName("Create")]
        //public async Task<IActionResult> Create([Bind("categoryName,categoryDescription")] ProductCategory category)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _dbcontext.Add(category);
        //            await _dbcontext.SaveChangesAsync();
        //            return RedirectToAction(nameof(Index));
        //        }
        //    }
        //    catch (DbUpdateException)
        //    {
        //        //Log the error (uncomment ex variable name and write a log.
        //        ModelState.AddModelError("", "Unable to save changes. " +
        //            "Try again, and if the problem persists " +
        //            "see your system administrator.");
        //    }
        //    return View(category);
        //}
        //[HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    var category = await _dbcontext.productCategories.FirstOrDefaultAsync(m => m.categoryID == id);

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var category = await _dbcontext.productCategories.FindAsync(id);
        //    _dbcontext.productCategories.Remove(category);
        //    await _dbcontext.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}


    }
}

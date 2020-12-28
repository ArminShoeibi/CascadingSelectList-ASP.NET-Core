using CascadingSelectList.Data;
using CascadingSelectList.Domain;
using CascadingSelectList.DTOs.Category;
using CascadingSelectList.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryService _categoryService;

        public CategoriesController(ApplicationDbContext db, CategoryService categoryService)
        {
            _db = db;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _db.Categories.Include(c => c.Parent).ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> CreateCategory()
        {

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = new()
                {
                    Name = dto.Name,
                    Description = dto.Description,
                    ParentCategoryId = dto.ParentCategoryId,
                };

                await _db.Categories.AddAsync(newCategory);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            return View(dto);
        }

        public async Task<IActionResult> EditCategory(int id)
        {

            UpdateCategoryDto categoryToUpdate = await _db.Categories.Where(c => c.CategoryId == id)
           .Select(c => new UpdateCategoryDto
           {
               CategoryId = c.CategoryId,
               Description = c.Description,
               Name = c.Name,
               ParentCategoryId = c.ParentCategoryId,

           }).FirstOrDefaultAsync();


            if (categoryToUpdate is null)
            {
                return NotFound();
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            return View(categoryToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(UpdateCategoryDto dto)
        {

            if (ModelState.IsValid)
            {
                Category categoryToUpdate = await _db.Categories.FindAsync(dto.CategoryId);
                if (categoryToUpdate is null)
                {
                    return NotFound();
                }
                categoryToUpdate.Name = dto.Name;
                categoryToUpdate.Description = dto.Description;
                categoryToUpdate.ParentCategoryId = dto.ParentCategoryId;
                categoryToUpdate.DateModified = DateTimeOffset.Now;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            return View(dto);
     
        }


        public async Task<IActionResult> GetChildCategoriesByParentCategoryId(int id)
        {
            var childCategories = await _categoryService.GetChildCategoriesByParentCategoryIdInSelectList(id);
            return Json(childCategories);
              
        }

    }
}

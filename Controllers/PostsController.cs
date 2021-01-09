using CascadingSelectList.Data;
using CascadingSelectList.Domain;
using CascadingSelectList.DTOs.Post;
using CascadingSelectList.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly CategoryService _categoryService;

        public PostsController(ApplicationDbContext db, CategoryService categoryService)
        {
            _db = db;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            List<Post> posts = await _db.Posts.Include(p => p.Category).ThenInclude(p => p.Parent).ToListAsync(); // Bad Practice use Select Loading
            return View(posts);
        }

   
        public async Task<IActionResult> CreatePost()
        {
            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                Post newPost = new()
                {
                    CategoryId = dto.CategoryId,
                    Text = dto.Text,
                    Title = dto.Title,
                };

                await _db.Posts.AddAsync(newPost);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            ViewBag.ChildCateogories = await _categoryService.GetChildCategoriesByParentCategoryIdInSelectList(dto.ParentCategoryId);
            return View(dto);
        }

      
        public async Task<IActionResult> EditPost(int id)
        {
           UpdatePostDto postToUpdate =  await _db.Posts.Where(p => p.PostId == id)
                 .Select(p => new UpdatePostDto 
                 { 
                    PostId = p.PostId,
                    CategoryId = p.PostId,
                    ParentCategoryId = p.Category.ParentCategoryId.GetValueOrDefault(),
                    Text = p.Text,
                    Title = p.Title
                 })
                 .FirstOrDefaultAsync();

            if (postToUpdate is null)
            {
                return NotFound();
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            ViewBag.ChildCategories = await _categoryService.GetChildCategoriesByParentCategoryIdInSelectList(postToUpdate.ParentCategoryId);
            return View(postToUpdate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(UpdatePostDto dto)
        {
            if (ModelState.IsValid)
            {
                Post postToUpdate = await _db.Posts.FindAsync(dto.PostId);
                if (postToUpdate is null)
                {
                    return NotFound();
                }
                postToUpdate.Text = dto.Text;

                postToUpdate.Title = dto.Title;

                postToUpdate.CategoryId = dto.CategoryId;
                postToUpdate.DateModified = DateTimeOffset.Now;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.ParentCategories = await _categoryService.GetParentCategoriesInSelectList();
            ViewBag.ChildCategories = await _categoryService.GetChildCategoriesByParentCategoryIdInSelectList(dto.ParentCategoryId);
            return View(dto);

        }
    }
}

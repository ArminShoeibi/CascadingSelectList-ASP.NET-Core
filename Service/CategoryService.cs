using CascadingSelectList.Data;
using CascadingSelectList.DTOs.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.Service
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<SelectList> GetParentCategoriesInSelectList()
        {
            var parentCategories = await _db.Categories.Where(c => c.ParentCategoryId == null)
                .Select(c => new { c.CategoryId, c.Name })
                .ToListAsync();

            return new SelectList(parentCategories, "CategoryId", "Name");
        }


        public async Task<SelectList> GetChildCategoriesByParentCategoryIdInSelectList(int parentCategoryId)
        {

            var childCategories = await _db.Categories.Where(c => c.ParentCategoryId == parentCategoryId)
              .Select(c => new { c.CategoryId, c.Name })
              .ToListAsync();

            return new SelectList(childCategories, "CategoryId", "Name");
        }
    }
}

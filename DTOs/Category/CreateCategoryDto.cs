using System.ComponentModel.DataAnnotations;

namespace CascadingSelectList.DTOs.Category
{
    public class CreateCategoryDto
    {
        [Display(Name = "Parent Category")]
        public int? ParentCategoryId { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(150)]
        public string Description { get; set; }

    }
}

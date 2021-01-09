using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.DTOs.Post
{
    public class CreatePostDto
    {
        [Required]
        [Display(Name = "Parent Category")]
        public int ParentCategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }


        [Required]
        [StringLength(70)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }

    }
}

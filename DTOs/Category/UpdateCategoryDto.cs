using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.DTOs.Category
{
    public class UpdateCategoryDto : CreateCategoryDto
    {
        public int CategoryId { get; set; }
    }
}

using CascadingSelectList.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.Domain
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Category Parent { get; set; }
        public ICollection<Category> Children { get; set; }
        public ICollection<Post> Posts { get; set; }
      
    }
}


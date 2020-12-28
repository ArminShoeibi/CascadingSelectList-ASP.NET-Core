using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CascadingSelectList.DTOs.Post
{
    public class UpdatePostDto : CreatePostDto
    {
        public int PostId { get; set; }
    }
}

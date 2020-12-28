using CascadingSelectList.Domain.Base;

namespace CascadingSelectList.Domain
{
    public class Post : BaseEntity
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }

        public Category Category { get; set; }
    }
}

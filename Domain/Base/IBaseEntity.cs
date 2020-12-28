using System;

namespace CascadingSelectList.Domain.Base
{
    public interface IBaseEntity
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
}

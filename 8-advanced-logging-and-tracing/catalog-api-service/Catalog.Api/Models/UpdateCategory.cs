using System;

namespace Catalog.Api.Models
{
    public class UpdateCategory : BaseModel
    {
        public Guid? ParentCategoryId { get; set; }
    }
}

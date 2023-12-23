using System;
using System.Collections.Generic;

namespace Catalog.Api.Models
{
    public class Category : BaseModel
    {
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public IList<Product> Products { get; set; }
    }
}

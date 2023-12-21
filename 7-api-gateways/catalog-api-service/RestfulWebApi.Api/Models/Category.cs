using System;
using System.Collections.Generic;

namespace RestfulWebApi.Api.Models
{
    public class Category : BaseModel
    {
        public Guid Id { get; set; }

        public Guid? ParentCategoryId { get; set; }

        public IList<Product> Products { get; set; }
    }
}

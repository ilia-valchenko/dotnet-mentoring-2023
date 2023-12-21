using System.Collections.Generic;
using System;

namespace RestfulWebApi.Api.Models
{
    public class CreateCategory : BaseModel
    {
        public Guid? ParentCategoryId { get; set; }

        public IList<CreateProduct> Products { get; set; }
    }
}

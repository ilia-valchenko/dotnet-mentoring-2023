using System;
using System.Collections.Generic;

namespace RestfulWebApi.UseCase.DTOs
{
    public class Category : BaseDto
    {
        public Guid? ParentCategoryId { get; set; }
        public IList<Product> Products { get; set; }
    }
}

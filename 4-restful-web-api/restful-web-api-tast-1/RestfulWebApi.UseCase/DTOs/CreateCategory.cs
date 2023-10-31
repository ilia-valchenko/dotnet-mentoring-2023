using System.Collections.Generic;
using System;

namespace RestfulWebApi.UseCase.DTOs
{
    public class CreateCategory : BaseDto
    {
        public Guid? ParentCategoryId { get; set; }

        public IList<CreateProduct> Products { get; set; }
    }
}

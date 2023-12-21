using System;

namespace RestfulWebApi.UseCase.DTOs
{
    public class UpdateCategory : BaseDto
    {
        public Guid? ParentCategoryId { get; set; }
    }
}

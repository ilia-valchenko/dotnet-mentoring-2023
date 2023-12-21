using System;

namespace RestfulWebApi.Api.Models
{
    public class UpdateCategory : BaseModel
    {
        public Guid? ParentCategoryId { get; set; }
    }
}

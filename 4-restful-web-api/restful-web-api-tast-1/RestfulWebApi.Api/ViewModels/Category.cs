using System;

namespace RestfulWebApi.Api.ViewModels
{
    public class Category : BaseViewModel
    {
        public Guid? ParentCategoryId { get; set; }
    }
}

using System.Collections.Generic;

namespace RestfulWebApi.Infrastructure.Entities
{
    internal class Category : BaseEntity
    {
        public string ParentCategoryId { get; set; }

        public IList<Product> Products { get; set; }
    }
}

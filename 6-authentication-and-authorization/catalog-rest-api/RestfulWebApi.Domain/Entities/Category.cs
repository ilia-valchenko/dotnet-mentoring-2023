using System;
using System.Collections.Generic;

namespace RestfulWebApi.Domain.Entities
{
    public class Category : BaseEntity
    {
        private readonly IList<Product> _products;

        public Category()
        {
            _products = new List<Product>();
        }

        public Guid? ParentCategoryId { get; set; }

        public IList<Product> Products => _products;
    }
}

using System;
using System.Collections.Generic;

namespace LayeredArchitecture.Domain.Entities
{
    public class Category : BaseEntity
    {
        private readonly IList<Product> _products;

        public Category(Guid id) : base(id)
        {
            _products = new List<Product>();
        }

        public Guid? ParentCategoryId { get; set; }

        public IList<Product> Products => _products;
    }
}

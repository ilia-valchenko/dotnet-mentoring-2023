using System;

namespace RestfulWebApi.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product(Guid id) : base(id)
        {
        }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

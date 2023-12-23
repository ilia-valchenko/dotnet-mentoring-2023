using System;

namespace Catalog.Api.Models
{
    public class Product : BaseModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ManufacturerId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

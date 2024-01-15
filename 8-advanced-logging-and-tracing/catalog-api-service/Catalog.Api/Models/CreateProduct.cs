using System;

namespace Catalog.Api.Models
{
    public class CreateProduct : BaseModel
    {
        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ManufacturerId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

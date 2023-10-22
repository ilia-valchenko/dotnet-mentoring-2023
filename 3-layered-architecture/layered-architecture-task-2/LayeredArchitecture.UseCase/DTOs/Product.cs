using System;

namespace LayeredArchitecture.UseCase.DTOs
{
    public class Product : BaseDto
    {
        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

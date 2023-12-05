using System;

namespace RestfulWebApi.UseCase.DTOs
{
    public class Product : BaseDto
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

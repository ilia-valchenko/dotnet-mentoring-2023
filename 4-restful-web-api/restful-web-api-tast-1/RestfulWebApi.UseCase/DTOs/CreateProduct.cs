using System;

namespace RestfulWebApi.UseCase.DTOs
{
    public class CreateProduct : BaseDto
    {
        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

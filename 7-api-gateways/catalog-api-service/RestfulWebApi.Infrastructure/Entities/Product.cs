namespace RestfulWebApi.Infrastructure.Entities
{
    internal class Product : BaseEntity
    {
        public string Description { get; set; }

        public string CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

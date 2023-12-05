namespace RestfulWebApi.UseCase.DTOs
{
    public class UpdateProduct : BaseDto
    {
        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

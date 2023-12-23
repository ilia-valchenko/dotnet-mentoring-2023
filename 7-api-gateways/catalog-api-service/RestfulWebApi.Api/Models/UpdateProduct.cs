namespace RestfulWebApi.Api.Models
{
    public class UpdateProduct : BaseModel
    {
        public string Description { get; set; }

        public decimal? Price { get; set; }

        public int? Amount { get; set; }
    }
}

using System;

namespace RestfulWebApi.Api.ViewModels
{
    public class Product : BaseViewModel
    {
        public string Description { get; set; }

        public Guid CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Amount { get; set; }
    }
}

using System;

namespace RestfulWebApi.Api.ViewModels
{
    public class BaseViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrlText { get; set; }
    }
}

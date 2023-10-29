using System;

namespace RestfulWebApi.UseCase.DTOs
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrlText { get; set; }
    }
}

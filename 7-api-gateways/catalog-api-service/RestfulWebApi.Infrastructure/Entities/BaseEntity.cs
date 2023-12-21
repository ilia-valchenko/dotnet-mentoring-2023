namespace RestfulWebApi.Infrastructure.Entities
{
    // Note: Infrastructure layer entities were introduced due to the following issue:
    // https://stackoverflow.com/questions/5898988/map-string-to-guid-with-dapper
    internal class BaseEntity
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}

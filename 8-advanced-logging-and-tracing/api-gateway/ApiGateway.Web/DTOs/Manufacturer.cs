﻿namespace ApiGateway.Web.DTOs;

public class Manufacturer
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime EstablishDate { get; set; }

    public string WikiLink { get; set; }

    public IEnumerable<Product> Products { get; set; }
}
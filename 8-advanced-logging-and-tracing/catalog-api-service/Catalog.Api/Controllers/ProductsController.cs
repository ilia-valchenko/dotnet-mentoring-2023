using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Product>))]
        [Authorize(Roles = "manager, buyer")]
        public async Task<IActionResult> GetAsync([FromQuery]Guid? manufacturerId, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information("[Catalog API] Starting to get a list of all products.");

            IEnumerable<Product> products = StaticData.StaticData.GetAllProducts();

            if (manufacturerId != null)
            {
                products = products.Where(p => p.ManufacturerId == manufacturerId);
            }

            return Ok(products);
        }

        [HttpGet("products/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [Authorize(Roles = "manager, buyer")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information($"[Catalog API] Starting to get a single product by id: '{id.ToString()}'.");

            var product = StaticData.StaticData.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Product>))]
        [Authorize(Roles = "manager, buyer")]
        public async Task<IActionResult> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information($"[Catalog API] Starting to get a list of products by category id: '{categoryId.ToString()}'.");

            var products = StaticData.StaticData.GetProductsByCategoryId(categoryId);
            return Ok(products);
        }

        [HttpPost("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> CreateAsync(Guid categoryId, CreateProduct productToCreate, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information("[Catalog API] Starting to create a new product. " +
                $"CategoryId: '{categoryId.ToString()}'. " +
                $"ManufacturerId: '{productToCreate.ManufacturerId}'. " +
                $"Name: '{productToCreate.Name}'. " +
                $"Description: '{productToCreate.Description}'. " +
                $"ImageUrl: '{productToCreate.ImageUrlText}'. " +
                $"Price: '{productToCreate.Price}'. " +
                $"Amount: '{productToCreate.Amount}'.");

            var category = StaticData.StaticData.GetCategoryById(categoryId);

            category.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = productToCreate.Name,
                CategoryId = category.Id,
                ManufacturerId = productToCreate.ManufacturerId,
                ImageUrlText = productToCreate.ImageUrlText,
                Description = productToCreate.Description,
                Amount = productToCreate.Amount,
                Price = productToCreate.Price
            });

            return Ok();
        }

        [HttpPatch("categories/{categoryId:Guid}/products/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "manager, buyer")]
        public async Task<IActionResult> UpdateAsync(
            Guid categoryId,
            Guid productId,
            UpdateProduct productToUpdate,
            CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information("[Catalog API] Starting to update an existing product. " +
                $"ProductId: '{productId.ToString()}'. " +
                $"CategoryId: '{categoryId.ToString()}'. " +
                $"NewName: '{productToUpdate.Name}'. " +
                $"NewDescription: '{productToUpdate.Description}'. " +
                $"NewImageUrlL: '{productToUpdate.ImageUrlText}'. " +
                $"NewPrice: '{productToUpdate.Price}'. " +
                $"NewAmount: '{productToUpdate.Amount}'.");

            var product = StaticData.StaticData.GetProductById(productId);

            if (productToUpdate.ManufacturerId != null)
            {
                product.ManufacturerId = productToUpdate.ManufacturerId.Value;
            }

            if (!string.IsNullOrWhiteSpace(productToUpdate.Name))
            {
                product.Name = productToUpdate.Name;
            }

            if (!string.IsNullOrWhiteSpace(productToUpdate.ImageUrlText))
            {
                product.ImageUrlText = productToUpdate.ImageUrlText;
            }

            if (!string.IsNullOrWhiteSpace(productToUpdate.Description))
            {
                product.Description = productToUpdate.Description;
            }

            if (productToUpdate.Price != null)
            {
                product.Price = productToUpdate.Price.Value;
            }

            if (productToUpdate.Amount != null)
            {
                product.Amount = productToUpdate.Amount.Value;
            }

            return new OkObjectResult(product);
        }

        [HttpDelete("categories/{categoryId:Guid}/products/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "manager, buyer")]
        public async Task<IActionResult> DeleteAsync(Guid categoryId, Guid productId, CancellationToken cancellationToken = default)
        {
            Serilog.Log.Information("[Catalog API] Starting to delete an existing product. " +
                $"ProductId: '{productId.ToString()}'. " +
                $"CategoryId: '{categoryId.ToString()}'.");

            var product = StaticData.StaticData.GetProductById(productId);
            var category = StaticData.StaticData.GetCategoryById(categoryId);

            category.Products.Remove(product);

            return new OkResult();
        }
    }
}

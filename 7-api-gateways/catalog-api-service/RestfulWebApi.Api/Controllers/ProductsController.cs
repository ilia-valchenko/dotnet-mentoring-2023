﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Api.Models;

namespace RestfulWebApi.Api.Controllers
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
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(
            //int pageNumber = DefaultPageNumber,
            //int pageSize = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var products = StaticData.StaticData.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("products/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = StaticData.StaticData.GetProductById(id);
            return Ok(product);
        }

        [HttpGet("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Product>))]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [AllowAnonymous]
        public async Task<IActionResult> GetByCategoryIdAsync(
            Guid categoryId,
            //int pageNumber = DefaultPageNumber,
            //int pageSize = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var products = StaticData.StaticData.GetProductsByCategoryId(categoryId);
            return Ok(products);
        }

        [HttpPost("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [AllowAnonymous]
        public async Task<ActionResult> CreateAsync(Guid categoryId, CreateProduct productToCreate, CancellationToken cancellationToken = default)
        {
            var category = StaticData.StaticData.GetCategoryById(categoryId);

            category.Products.Add(new Product
            {
                Id = Guid.NewGuid(),
                Name = productToCreate.Name,
                CategoryId = category.Id,
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
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync(
            Guid categoryId,
            Guid productId,
            UpdateProduct productToUpdate,
            CancellationToken cancellationToken = default)
        {
            var product = StaticData.StaticData.GetProductById(productId);

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
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid categoryId, Guid productId, CancellationToken cancellationToken = default)
        {
            var product = StaticData.StaticData.GetProductById(productId);
            var category = StaticData.StaticData.GetCategoryById(categoryId);

            category.Products.Remove(product);

            return new OkResult();
        }
    }
}

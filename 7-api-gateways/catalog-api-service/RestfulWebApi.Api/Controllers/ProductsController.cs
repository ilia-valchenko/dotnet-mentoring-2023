using System;
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
            var createdProduct = await _productService.CreateAsync(productToCreate, cancellationToken);
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
            var updatedProduct = await _productService.UpdateAsync(productId, productToUpdate, cancellationToken);
            return new OkObjectResult(updatedProduct);
        }

        [HttpDelete("categories/{categoryId:Guid}/products/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid categoryId, Guid productId, CancellationToken cancellationToken = default)
        {
            await _productService.DeleteAsync(productId, cancellationToken);
            return new OkResult();
        }
    }
}

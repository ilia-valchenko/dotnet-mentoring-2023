using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.UseCase.Services.Interfaces;

namespace RestfulWebApi.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UseCase.DTOs.Product>))]
        [Authorize]
        public async Task<IActionResult> GetAllAsync(
            int pageNumber = DefaultPageNumber,
            int pageSize = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return Ok(products);
        }

        [HttpGet("products/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Product))]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productService.GetByIdAsync(id, cancellationToken);
            return Ok(product);
        }

        [HttpGet("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UseCase.DTOs.Product>))]
        [Authorize]
        public async Task<IActionResult> GetByCategoryIdAsync(
            Guid categoryId,
            int pageNumber = DefaultPageNumber,
            int pageSize = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetByCategoryIdAsync(categoryId, pageNumber, pageSize, cancellationToken);
            return Ok(products);
        }

        [HttpPost("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult>/*Task<IActionResult>*/ CreateAsync(Guid categoryId, UseCase.DTOs.CreateProduct productToCreate, CancellationToken cancellationToken = default)
        {
            var createdProduct = await _productService.CreateAsync(productToCreate, cancellationToken);
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = createdProduct.Id }, createdProduct);
            return Ok();
        }

        [HttpPatch("categories/{categoryId:Guid}/products/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(
            Guid categoryId,
            Guid productId,
            UseCase.DTOs.UpdateProduct productToUpdate,
            CancellationToken cancellationToken = default)
        {
            var updatedProduct = await _productService.UpdateAsync(productId, productToUpdate, cancellationToken);
            return new OkObjectResult(updatedProduct);
        }

        [HttpDelete("categories/{categoryId:Guid}/products/{productId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid categoryId, Guid productId, CancellationToken cancellationToken = default)
        {
            await _productService.DeleteAsync(productId, cancellationToken);
            return new OkResult();
        }
    }
}

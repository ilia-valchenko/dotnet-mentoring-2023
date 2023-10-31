using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetAllAsync(cancellationToken);
            return Ok(products);
        }

        [HttpGet("products/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Product))]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _productService.GetByIdAsync(id, cancellationToken);
            return Ok(product);
        }

        [HttpGet("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UseCase.DTOs.Product>))]
        public async Task<IActionResult> GetBYCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetByCategoryIdAsync(categoryId, cancellationToken);
            return Ok(products);
        }

        [HttpPost("categories/{categoryId:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Guid categoryId, UseCase.DTOs.Product product, CancellationToken cancellationToken = default)
        {
            var createdProduct = await _productService.CreateAsync(product, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdProduct.Id }, createdProduct);
        }
    }
}

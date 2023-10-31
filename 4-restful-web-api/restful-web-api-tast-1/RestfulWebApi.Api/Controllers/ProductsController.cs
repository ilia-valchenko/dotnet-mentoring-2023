using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Api.ViewModels;
using RestfulWebApi.UseCase.Services;
using RestfulWebApi.UseCase.Services.Interfaces;

namespace RestfulWebApi.Api.Controllers
{
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IProductService productService,
            IMapper mapper,
            ILogger<ProductsController> logger)
        {
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Product>))]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            var products = _productService.GetAsync(cancellationToken);
            return Ok(_mapper.Map<IList<Product>>(products));
        }

        [HttpGet("categories/{id:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Product>))]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var products = await _productService.GetByCategoryIdAsync(id, cancellationToken);
            return Ok(_mapper.Map<IList<Product>>(products));
        }

        [HttpPost("products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var productToCreate = _mapper.Map<UseCase.DTOs.Product>(product);
            var createdProduct = await _productService.CreateAsync(productToCreate, cancellationToken);
            return CreatedAtAction(nameof(GetAsync), new { id = createdProduct.Id }, _mapper.Map<Product>(createdProduct));
        }

        [HttpPost("categories/{id:Guid}/products")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Guid id, Product product, CancellationToken cancellationToken = default)
        {
            var productToCreate = _mapper.Map<UseCase.DTOs.Product>(product);
            var createdProduct = await _productService.CreateAsync(productToCreate, cancellationToken);
            return CreatedAtAction(nameof(GetAsync), new { id = createdProduct.Id }, _mapper.Map<Product>(createdProduct));
        }
    }
}

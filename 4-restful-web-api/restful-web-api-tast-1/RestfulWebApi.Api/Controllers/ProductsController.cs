using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Api.ViewModels;
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
    }
}

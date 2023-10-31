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
    public class CategoriesController : BaseController
    {
        private readonly IService<UseCase.DTOs.Category> _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            IService<UseCase.DTOs.Category> categoryService,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UseCase.DTOs.Category>))]
        public async Task<IActionResult> GetAllAsync(int pageNumber = 1, CancellationToken cancellationToken = default)
        {
            var categories = await _categoryService.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpGet("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(UseCase.DTOs.Category category, CancellationToken cancellationToken = default)
        {
            var createdCategory = await _categoryService.CreateAsync(category, cancellationToken);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCategory.Id }, createdCategory);
        }
    }
}

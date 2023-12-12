using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.UseCase.Services.Interfaces;

namespace RestfulWebApi.Api.Controllers
{
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ICategoryService categoryService,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<UseCase.DTOs.Category>))]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetAllAsync(
            int pageNumber = DefaultPageNumber,
            int pageSize = DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            var claims = User.Claims.ToList();
            var categories = await _categoryService.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return Ok(categories);
        }

        [HttpGet("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
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
        [Authorize]
        //[Authorize("writePolicy")]
        public async Task<IActionResult> CreateAsync(UseCase.DTOs.CreateCategory categoryToCreate, CancellationToken cancellationToken = default)
        {
            var createdCategory = await _categoryService.CreateAsync(categoryToCreate, cancellationToken);
            return Ok();
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPatch("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UseCase.DTOs.Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(Guid id, UseCase.DTOs.UpdateCategory categoryToUpdate, CancellationToken cancellationToken = default)
        {
            var updatedCategory = await _categoryService.UpdateAsync(id, categoryToUpdate, cancellationToken);
            return new OkObjectResult(updatedCategory);
        }

        [HttpDelete("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _categoryService.DeleteAsync(id, cancellationToken);
            return new OkResult();
        }
    }
}

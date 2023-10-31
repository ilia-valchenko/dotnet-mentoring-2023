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
    public class CategoriesController : BaseController
    {
        private readonly IService<UseCase.DTOs.Category> _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            IService<UseCase.DTOs.Category> categoryService,
            IMapper mapper,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Category>))]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryService.GetAsync(cancellationToken);
            return Ok(_mapper.Map<IList<Category>>(categories));
        }

        [HttpGet("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = await _categoryService.GetAsync(id, cancellationToken);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<Category>(category));
        }

        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var categoryToCreate = _mapper.Map<UseCase.DTOs.Category>(category);
            var createdCategory = await _categoryService.CreateAsync(categoryToCreate, cancellationToken);
            return CreatedAtAction(nameof(GetAsync), new { id = createdCategory.Id }, _mapper.Map<Category>(createdCategory));
        }
    }
}

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
    public class CategoriesController : BaseController
    {
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("categories")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Category>))]
        //[Authorize] // Now this Authorize attribute will always go to the JwtRequirementHandler.
        //[Authorize(Roles = "manager, buyer")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var claims = User.Claims.ToList();
            var categories = StaticData.StaticData.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "manager, buyer")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var category = StaticData.StaticData.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost("categories")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[Authorize(Roles = "manager")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAsync(CreateCategory categoryToCreate, CancellationToken cancellationToken = default)
        {
            var categoryId = Guid.NewGuid();

            var category = new Category
            {
                Id = categoryId,
                Name = categoryToCreate.Name,
                ImageUrlText = categoryToCreate.ImageUrlText,
                Products = new List<Product>()
            };

            foreach (var createProduct in categoryToCreate.Products)
            {
                category.Products.Add(new Product
                {
                    Id = Guid.NewGuid(),
                    CategoryId = categoryId,
                    Name = createProduct.Name,
                    ImageUrlText = createProduct.ImageUrlText,
                    Description = createProduct.Description,
                    Amount = createProduct.Amount,
                    Price = createProduct.Price
                });
            }

            StaticData.StaticData.CreateCategory(category);
            return Ok();
        }

        [HttpPatch("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "manager")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategory categoryToUpdate, CancellationToken cancellationToken = default)
        {
            var category = StaticData.StaticData.GetCategoryById(id);

            if (categoryToUpdate.ParentCategoryId != null)
            {
                category.ParentCategoryId = categoryToUpdate.ParentCategoryId;
            }

            if (!string.IsNullOrWhiteSpace(categoryToUpdate.Name))
            {
                category.Name = categoryToUpdate.Name;
            }

            if (!string.IsNullOrWhiteSpace(categoryToUpdate.ImageUrlText))
            {
                category.ImageUrlText = categoryToUpdate.ImageUrlText;
            }

            return new OkObjectResult(category);
        }

        [HttpDelete("categories/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize(Roles = "manager")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            StaticData.StaticData.DeleteCategory(id);
            return new OkResult();
        }
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestfulWebApi.Api.ViewModels;
using RestfulWebApi.UseCase.Services.Interfaces;

namespace RestfulWebApi.Api.Controllers
{
    public class CatalogsController : BaseController
    {
        private readonly IService<UseCase.DTOs.Category> _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogsController> _logger;

        public CatalogsController(
            IService<UseCase.DTOs.Category> categoryService,
            IMapper mapper,
            ILogger<CatalogsController> logger)
        {
            _categoryService = categoryService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _categoryService.GetAsync(cancellationToken);
            return _mapper.Map<IEnumerable<Category>>(categories);
        }

        [HttpPost]
        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var categoryToCreate = _mapper.Map<UseCase.DTOs.Category>(category);
            var createdCategory = await _categoryService.CreateAsync(categoryToCreate, cancellationToken);
            return _mapper.Map<Category>(createdCategory);
        }
    }
}

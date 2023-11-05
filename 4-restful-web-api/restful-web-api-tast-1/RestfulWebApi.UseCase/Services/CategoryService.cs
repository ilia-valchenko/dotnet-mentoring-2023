using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RestfulWebApi.UseCase.Exceptions;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.UseCase.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IValidator<UseCase.DTOs.BaseDto> _validator;
        private readonly IRepository<Domain.Entities.Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IValidator<UseCase.DTOs.BaseDto> validator, IRepository<Domain.Entities.Category> repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UseCase.DTOs.Category> CreateAsync(UseCase.DTOs.CreateCategory categoryToCreate, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(categoryToCreate);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Provided category is not valid.");
            }

            var category = _mapper.Map<UseCase.DTOs.Category>(categoryToCreate);
            category.Id = Guid.NewGuid();

            if (category.Products != null && category.Products.Any())
            {
                foreach (var product in category.Products)
                {
                    product.Id = Guid.NewGuid();
                }
            }

            var createdCategory = await _repository.CreateAsync(_mapper.Map<Domain.Entities.Category>(category), cancellationToken);
            return _mapper.Map<UseCase.DTOs.Category>(createdCategory);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<UseCase.DTOs.Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UseCase.DTOs.Category>(entity);
        }

        public async Task<IList<UseCase.DTOs.Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<IList<UseCase.DTOs.Category>>(entities);
        }

        public async Task<UseCase.DTOs.Category> UpdateAsync(Guid id, UseCase.DTOs.UpdateCategory categoryToUpdate, CancellationToken cancellationToken = default)
        {
            var existingCategory = await GetByIdAsync(id, cancellationToken);

            if (existingCategory == null)
            {
                throw new ResourceNotFoundException<Domain.Entities.Category>($"Can't find a category with id: '{id.ToString()}'.");
            }

            var validationResult = _validator.Validate(categoryToUpdate);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Provided category is not valid.");
            }

            existingCategory.Name = categoryToUpdate.Name;
            existingCategory.ImageUrlText = categoryToUpdate.ImageUrlText;
            existingCategory.ParentCategoryId = categoryToUpdate.ParentCategoryId;

            var updatedCategory = _repository.UpdateAsync(_mapper.Map<Domain.Entities.Category>(existingCategory), cancellationToken);
            return _mapper.Map<UseCase.DTOs.Category>(updatedCategory);
        }
    }
}

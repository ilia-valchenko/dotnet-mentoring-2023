using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RestfulWebApi.UseCase.DTOs;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.UseCase.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IValidator<Category> _validator;
        private readonly IRepository<Domain.Entities.Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IValidator<Category> validator, IRepository<Domain.Entities.Category> repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(category);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            var createdCategory = await _repository.CreateAsync(_mapper.Map<Domain.Entities.Category>(category), cancellationToken);
            return _mapper.Map<Category>(createdCategory);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<Category>(entity);
        }

        public async Task<IList<Category>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<IList<Category>>(entities);
        }

        public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(category);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            await _repository.UpdateAsync(_mapper.Map<Domain.Entities.Category>(category), cancellationToken);
        }
    }
}

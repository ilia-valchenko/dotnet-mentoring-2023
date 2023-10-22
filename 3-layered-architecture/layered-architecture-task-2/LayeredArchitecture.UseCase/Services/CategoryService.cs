using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services.Interfaces;
using LayeredArchitecture.UseCase.Validators.Interfaces;

namespace LayeredArchitecture.UseCase.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IValidator<Category> _validator;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(IValidator<Category> validator, IRepository repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(Category item, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            await _repository.CreateAsync(_mapper.Map<Domain.Entities.Category>(item), cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetAsync(id, cancellationToken);
            return _mapper.Map<Category>(entity);
        }

        public async Task<IList<Category>> GetAsync(CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAsync(cancellationToken);
            return _mapper.Map<IList<Category>>(entities);
        }

        public async Task UpdateAsync(Category item, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            await _repository.UpdateAsync(_mapper.Map<Domain.Entities.Category>(item), cancellationToken);
        }
    }
}

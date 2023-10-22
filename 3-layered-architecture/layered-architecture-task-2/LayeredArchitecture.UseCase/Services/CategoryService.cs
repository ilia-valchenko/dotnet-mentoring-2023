using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LayeredArchitecture.UseCase.DTOs;
using LayeredArchitecture.UseCase.Services.Interfaces;
using LayeredArchitecture.UseCase.Validators.Interfaces;

namespace LayeredArchitecture.UseCase.Services
{
    public class CategoryService : IService<Category>
    {
        private readonly IValidator<Category> _validator;
        private readonly IRepository _repository;

        public CategoryService(IValidator<Category> validator, IRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task CreateAsync(Category item, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(item);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            // Map
            await _repository.CreateAsync(item, cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Category>> GetAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Category item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

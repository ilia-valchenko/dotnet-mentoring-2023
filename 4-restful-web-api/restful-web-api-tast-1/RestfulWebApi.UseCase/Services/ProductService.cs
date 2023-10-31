using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.UseCase.Services
{
    public class ProductService : IProductService
    {
        private readonly IValidator<DTOs.Product> _validator;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IValidator<DTOs.Product> validator, IProductRepository repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DTOs.Product> CreateAsync(DTOs.Product product, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(product);

            if (!validationResult.IsValid)
            {
                throw new ArgumentException("Provided category is not valid.");
            }

            var createdProduct = await _repository.CreateAsync(_mapper.Map<Domain.Entities.Product>(product), cancellationToken);
            return _mapper.Map<DTOs.Product>(createdProduct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<DTOs.Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetAsync(id, cancellationToken);
            return _mapper.Map<DTOs.Product>(product);
        }

        public async Task<IList<DTOs.Product>> GetAsync(CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetAsync(cancellationToken);
            return _mapper.Map<IList<DTOs.Product>>(products);
        }

        public async Task<IList<DTOs.Product>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetByCategoryIdAsync(categoryId, cancellationToken);
            return _mapper.Map<IList<DTOs.Product>>(products);
        }

        public Task UpdateAsync(DTOs.Product product, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

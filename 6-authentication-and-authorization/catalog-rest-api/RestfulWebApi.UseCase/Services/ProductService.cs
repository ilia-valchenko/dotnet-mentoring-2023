using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using RestfulWebApi.UseCase.Exceptions;
using RestfulWebApi.UseCase.Repositories;
using RestfulWebApi.UseCase.Services.Interfaces;
using RestfulWebApi.UseCase.Validators.Interfaces;

namespace RestfulWebApi.UseCase.Services
{
    public class ProductService : IProductService
    {
        private readonly IValidator<DTOs.BaseDto> _validator;
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IValidator<DTOs.BaseDto> validator, IProductRepository repository, IMapper mapper)
        {
            _validator = validator;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DTOs.Product> CreateAsync(DTOs.CreateProduct productToCreate, CancellationToken cancellationToken = default)
        {
            var validationResult = _validator.Validate(productToCreate);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Provided category is not valid.");
            }

            var product = _mapper.Map<DTOs.Product>(productToCreate);
            product.Id = Guid.NewGuid();

            var createdProduct = await _repository.CreateAsync(_mapper.Map<Domain.Entities.Product>(product), cancellationToken);
            return _mapper.Map<DTOs.Product>(createdProduct);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }

        public async Task<DTOs.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _repository.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<DTOs.Product>(product);
        }

        public async Task<IList<DTOs.Product>> GetAllAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetAllAsync(pageNumber, pageSize, cancellationToken);
            return _mapper.Map<IList<DTOs.Product>>(products);
        }

        public async Task<IList<DTOs.Product>> GetByCategoryIdAsync(Guid categoryId, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var products = await _repository.GetByCategoryIdAsync(categoryId, pageNumber, pageSize, cancellationToken);
            return _mapper.Map<IList<DTOs.Product>>(products);
        }

        public async Task<DTOs.Product> UpdateAsync(Guid id, DTOs.UpdateProduct productToUpdate, CancellationToken cancellationToken = default)
        {
            var existingProduct = await GetByIdAsync(id, cancellationToken);

            if (existingProduct == null)
            {
                throw new ResourceNotFoundException<Domain.Entities.Product>($"Can't find a product with id: '{id.ToString()}'.");
            }

            var validationResult = _validator.Validate(productToUpdate);

            if (!validationResult.IsValid)
            {
                throw new ValidationException("Provided product is not valid.");
            }

            existingProduct.Name = productToUpdate.Name;
            existingProduct.ImageUrlText = productToUpdate.ImageUrlText;
            existingProduct.Description = productToUpdate.Description;
            existingProduct.Amount = productToUpdate.Amount;

            var updatedProduct = _repository.UpdateAsync(_mapper.Map<Domain.Entities.Product>(existingProduct), cancellationToken);
            return _mapper.Map<DTOs.Product>(updatedProduct);
        }
    }
}

using AutoMapper;
using MediatR;
using MessageBasedArchitecture.Application.Models;
using MessageBasedArchitecture.Application.Services.Interfaces;
using MessageBasedArchitecture.Domain.Events;
using MessageBasedArchitecture.Domain.Exceptions;

namespace MessageBasedArchitecture.Application.Services;

public class Service : IService
{
    private readonly IRepository _repository;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public Service(IRepository repository, IMediator mediator, IMapper mapper)
    {
        _repository = repository;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var items = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<Item>>(items);
    }

    public async Task UpdateAsync(
        Guid id,
        UpdateItem updateItem,
        Guid correlationId,
        CancellationToken cancellationToken = default)
    {
        var item = await _repository.GetByIdAsync(id, cancellationToken);

        if (item == null)
        {
            throw new ResourceNotFoundException();
        }

        item.Name = updateItem.Name;
        item.Price = updateItem.Price;

        await _repository.UpdateAsync(item, cancellationToken);

        Serilog.Log.Information("[MessageSender API] Starting to publish a new message to the message broker. " +
            $"Id: '{item.Id.ToString()}'. " +
            $"Price: '{item.Price}'.");

        await _mediator.Publish(new PriceChanged
        {
            Id = item.Id,
            Price = item.Price,
            CorrelationId = correlationId
        }, cancellationToken);
    }
}

using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Tickets;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Tickets;
public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<Ticket>>
{
    private readonly ITicketRepository _repository;

    public GetTicketsQueryHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Ticket>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

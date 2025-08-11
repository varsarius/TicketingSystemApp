using MediatR;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Tickets;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Tickets;
public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, Ticket>
{
    private readonly ITicketRepository _repository;

    public GetTicketByIdQueryHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<Ticket> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

using MediatR;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Tickets;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.Tickets;
public class GetTicketsQueryHandler : IRequestHandler<GetTicketsQuery, List<TicketDto>>
{
    private readonly ITicketReadRepository _repository;

    public GetTicketsQueryHandler(ITicketReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TicketDto>> Handle(GetTicketsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllSortFilterAsync(
            request.SortBy,
            request.SortOrder,
            request.CategoryId,
            request.Status,
            request.Priority,
            request.UserId
        );
    }
}

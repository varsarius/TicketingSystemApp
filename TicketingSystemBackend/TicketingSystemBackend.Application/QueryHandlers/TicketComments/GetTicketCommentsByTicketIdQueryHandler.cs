using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.TicketComments;

namespace TicketingSystemBackend.Application.QueryHandlers.TicketComments;
public class GetTicketCommentsByTicketIdQueryHandler : IRequestHandler<GetTicketCommentsByTicketIdQuery, List<TicketCommentDto>>
{
    private readonly ITicketcommentReadRepository _repository;

    public GetTicketCommentsByTicketIdQueryHandler(ITicketcommentReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TicketCommentDto>> Handle(GetTicketCommentsByTicketIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllByTicketIdAsync(request.ticketId);
    }
}

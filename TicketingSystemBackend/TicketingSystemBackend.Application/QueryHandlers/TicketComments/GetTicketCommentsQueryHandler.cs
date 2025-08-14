using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.TicketComments;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.TicketComments;
public class GetTicketCommentsQueryHandler : IRequestHandler<GetTicketCommentsQuery, List<TicketComment>>
{
    private readonly ITicketCommentRepository _repository;

    public GetTicketCommentsQueryHandler(ITicketCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TicketComment>> Handle(GetTicketCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

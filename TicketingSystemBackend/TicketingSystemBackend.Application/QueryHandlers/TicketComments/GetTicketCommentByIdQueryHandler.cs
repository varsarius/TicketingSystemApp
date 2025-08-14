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
public class GetTicketCommentByIdQueryHandler : IRequestHandler<GetTicketCommentByIdQuery, TicketComment>
{
    private readonly ITicketCommentRepository _repository;

    public GetTicketCommentByIdQueryHandler(ITicketCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<TicketComment> Handle(GetTicketCommentByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

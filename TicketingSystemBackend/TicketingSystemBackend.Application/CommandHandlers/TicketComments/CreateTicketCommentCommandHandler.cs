using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketComments;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketComments;
public class CreateTicketCommentCommandHandler : IRequestHandler<CreateTicketCommentCommand>
{
    private readonly ITicketCommentRepository _repository;

    public CreateTicketCommentCommandHandler(ITicketCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateTicketCommentCommand request, CancellationToken cancellationToken)
    {
        var ticketComment = new TicketComment
        {
            Description = request.Description,
            UpdatedAt = null
        };
        await _repository.CreateAsync(ticketComment);
    }
}

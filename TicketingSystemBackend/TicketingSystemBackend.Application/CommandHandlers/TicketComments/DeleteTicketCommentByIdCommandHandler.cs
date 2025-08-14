using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketComments;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketComments;
public class DeleteTicketCommentByIdCommandHandler : IRequestHandler<DeleteTicketCommentByIdCommand>
{
    private readonly ITicketCommentRepository _repository;

    public DeleteTicketCommentByIdCommandHandler(ITicketCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTicketCommentByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

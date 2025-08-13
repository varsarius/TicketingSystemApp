using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketComments;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketComments;
public class UpdateTicketCommentCommandHandler : IRequestHandler<UpdateTicketCommentCommand>
{
    private readonly ITicketCommentRepository _repository;

    public UpdateTicketCommentCommandHandler(ITicketCommentRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateTicketCommentCommand request, CancellationToken cancellationToken)
    {
        var ticketComment = await _repository.GetByIdAsync(request.Id);
        ticketComment.Description = request.Description;
        ticketComment.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticketComment);
    }
}

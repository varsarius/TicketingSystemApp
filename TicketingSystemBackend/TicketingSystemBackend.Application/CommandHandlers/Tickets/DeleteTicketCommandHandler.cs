using MediatR;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
{
    private readonly ITicketRepository _repository;

    public DeleteTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

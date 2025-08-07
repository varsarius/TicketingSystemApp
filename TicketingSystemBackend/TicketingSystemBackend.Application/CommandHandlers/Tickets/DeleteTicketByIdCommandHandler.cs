using MediatR;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class DeleteTicketByIdCommandHandler : IRequestHandler<DeleteTicketByIdCommand>
{
    private readonly ITicketRepository _repository;

    public DeleteTicketByIdCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTicketByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

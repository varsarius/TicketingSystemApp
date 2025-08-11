using MediatR;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Interfaces;
namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand>
{
    private readonly ITicketRepository _repository;

    public UpdateTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id);
        ticket.Title = request.Title;
        ticket.Description = request.Description;
        ticket.Priority = request.Priority;
        ticket.CategoryId = request.CategoryId;
        ticket.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticket);
    }
}

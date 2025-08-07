using MediatR;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
{
    private readonly ITicketRepository _ticketRepository;

    public CreateTicketCommandHandler(ITicketRepository ITicketRepository)
    {
        _ticketRepository = ITicketRepository;
    }

    public async Task Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            UserId = request.UserId,
            AgentId = request.AgentId,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.Now,//REMOVE: this is no
            UpdatedAt = DateTime.Now
        };
        await _ticketRepository.CreateAsync(ticket);
    }
}

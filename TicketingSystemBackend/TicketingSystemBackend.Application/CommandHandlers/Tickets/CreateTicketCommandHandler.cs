using MediatR;
using TicketingSystemBackend.Application.Commands.Tickets;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
{
    private readonly ITicketRepository _ticketRepository;

    public CreateTicketCommandHandler(ITicketRepository ITicketRepository)
    {
        _ticketRepository = ITicketRepository;
    }

    public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = new Ticket
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            Status = request.Status,
            UserId = request.UserId,
            AgentId = request.AgentId,
            TicketCategoryId = request.TicketCategoryId
        };
        await _ticketRepository.CreateAsync(ticket);

        return ticket.Id;
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Api.Commands.Tickets;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.CommandHandlers.Tickets;
public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand>
{
    private readonly TicketRepository _ticketRepository;

    public CreateTicketCommandHandler(TicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
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
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _ticketRepository.CreateAsync(ticket);
    }
}

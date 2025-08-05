using MediatR;
using System.ComponentModel.DataAnnotations;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Application.Commands.Tickets;

public record CreateTicketCommand
(
    [Required] string Title,
    [Required] string Description,
    Priority Priority,
    int UserId,
    int AgentId,
    int CategoryId
) : IRequest;

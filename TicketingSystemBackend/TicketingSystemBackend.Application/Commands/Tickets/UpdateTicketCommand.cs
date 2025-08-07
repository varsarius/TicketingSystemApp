using MediatR;
using System.ComponentModel.DataAnnotations;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Application.Commands.Tickets;

public record UpdateTicketCommand(
    [Required] string Title,
    [Required] string Description,
    Priority Priority,
    int CategoryId,
    int Id
) : IRequest;

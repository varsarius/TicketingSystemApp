using System.ComponentModel.DataAnnotations;
using TicketingSystemFrontend.Client.Requests.Enums;

namespace TicketingSystemFrontend.Client.Requests.Commands.Tickets;

public record UpdateTicketCommand(
    [Required] string Title,
    [Required] string Description,
    Priority Priority,
    Status Status,
    int CategoryId,
    int Id
);

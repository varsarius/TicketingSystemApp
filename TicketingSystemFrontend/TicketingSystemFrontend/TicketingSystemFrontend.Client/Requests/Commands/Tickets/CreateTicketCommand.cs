using System.ComponentModel.DataAnnotations;
using TicketingSystemFrontend.Client.Requests.Enums;

namespace TicketingSystemFrontend.Client.Requests.Commands.Tickets;

public record CreateTicketCommand
(
    [Required] string Title,
    [Required] string Description,
    Priority Priority,
    Status Status,
    Guid UserId,
    Guid? AgentId,
    int TicketCategoryId
);

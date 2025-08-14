using System.ComponentModel.DataAnnotations;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemFrontend.Client.Requests;

public class TicketUpdateRequest
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public int CategoryId { get; set; }
    public int Id { get; set; }
}

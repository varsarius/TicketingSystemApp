using System.ComponentModel.DataAnnotations;
using TicketingSystemFrontend.Client.Requests.Enums;

namespace TicketingSystemFrontend.Client.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? AgentId { get; set; }
    public int TicketCategoryId { get; set; }

    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    public string CategoryName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string AgentName { get; set; } = null!;
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; set; }

}


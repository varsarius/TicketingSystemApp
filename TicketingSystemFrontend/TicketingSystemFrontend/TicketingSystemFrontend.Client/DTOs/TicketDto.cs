using TicketingSystemFrontend.Client.Requests.Enums;

namespace TicketingSystemFrontend.Client.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TicketCategoryName { get; set; } = string.Empty;
    public int TicketCategoryId { get; set; }
    public string? AgentName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public Guid? AgentId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Priority Priority { get; set; }
    public Status Status { get; set; }
    public int CategoryId { get; set; }


}


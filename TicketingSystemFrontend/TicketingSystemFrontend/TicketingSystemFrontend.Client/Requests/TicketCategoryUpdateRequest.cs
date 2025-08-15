using System.ComponentModel.DataAnnotations;

namespace TicketingSystemFrontend.Client.Requests;

public class TicketCategoryUpdateRequest
{
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; } = null!;

    
}

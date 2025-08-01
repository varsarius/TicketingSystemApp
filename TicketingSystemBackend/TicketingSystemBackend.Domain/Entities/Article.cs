using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public int ArticleId { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    public int UserAuthorId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }


}

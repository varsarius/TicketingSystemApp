using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Enums;

namespace TicketingSystemBackend.Domain.Entities;

internal class ArticleCategory
{
    public int Id { get; set; }
    public Category Category { get; set; }

}

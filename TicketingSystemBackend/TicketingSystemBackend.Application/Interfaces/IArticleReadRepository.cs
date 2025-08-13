using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IArticleReadRepository
{
    Task<List<ArticleDto>> GetAllAsync();
    Task<ArticleDto?> GetByIdAsync(int id);
}

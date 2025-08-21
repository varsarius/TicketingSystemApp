using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Application.Queries.Tickets.Files;

public class GetTicketFilesQuery : IRequest<List<ArticleFileDto>>
{
    public int ArticleId { get; set; }
    public GetTicketFilesQuery(int articleId) => ArticleId = articleId;
}
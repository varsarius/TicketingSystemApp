using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Application.Queries.Articles.Files;
public class GetArticleFilesQuery : IRequest<List<ArticleFileDto>>
{
    public int ArticleId { get; set; }
    public GetArticleFilesQuery(int articleId) => ArticleId = articleId;
}
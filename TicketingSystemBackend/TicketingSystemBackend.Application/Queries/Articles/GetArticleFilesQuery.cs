using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Queries.Articles;
public class GetArticleFilesQuery : IRequest<List<string>>
{
    public int ArticleId { get; set; }
    public GetArticleFilesQuery(int articleId) => ArticleId = articleId;
}
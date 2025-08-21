using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Queries.Articles.Files;
public record GetArticleFileByIdQuery(int ArticleId, int FileId) : IRequest<Domain.Entities.File?>;

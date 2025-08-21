using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace TicketingSystemBackend.Application.Commands.Articles.Files;
public class UploadArticleFilesCommand : IRequest
{
    public int ArticleId { get; set; }
    public List<IFormFile> Files { get; set; } = new();
}

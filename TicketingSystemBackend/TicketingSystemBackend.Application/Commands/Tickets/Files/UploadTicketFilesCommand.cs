using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Commands.Tickets.Files;

public class UploadTicketFilesCommand : IRequest
{
    public int TicketId { get; set; }
    public List<IFormFile> Files { get; set; } = new();
}

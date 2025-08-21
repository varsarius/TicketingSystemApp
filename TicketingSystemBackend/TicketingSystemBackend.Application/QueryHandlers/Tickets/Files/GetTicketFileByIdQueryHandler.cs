using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Articles.Files;
using TicketingSystemBackend.Application.Queries.Tickets.Files;

namespace TicketingSystemBackend.Application.QueryHandlers.Tickets.Files;

public class GetTicketFileByIdQueryHandler : IRequestHandler<GetTicketFileByIdQuery, Domain.Entities.File?>
{
    private readonly IFileRepository _fileRepository;

    public GetTicketFileByIdQueryHandler(IFileRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }

    public async Task<Domain.Entities.File?> Handle(GetTicketFileByIdQuery request, CancellationToken cancellationToken)
    {
        return await _fileRepository.GetFileByIdAndTicketIdAsync(request.FileId, request.TicketId, cancellationToken);
    }
}
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Queries.Tickets.Files;

public record GetTicketFileByIdQuery(int TicketId, int FileId) : IRequest<Domain.Entities.File?>;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs;

namespace TicketingSystemBackend.Application.Queries.TicketComments;
public record GetTicketCommentsByTicketIdQuery(int ticketId) : IRequest<List<TicketCommentDto>>;

using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.Queries.TicketComments;
public record GetTicketCommentByIdQuery(int Id) : IRequest<TicketComment>;

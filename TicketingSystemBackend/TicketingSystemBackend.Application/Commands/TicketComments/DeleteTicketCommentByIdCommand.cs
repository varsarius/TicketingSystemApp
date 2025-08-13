using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Commands.TicketComments;
public record DeleteTicketCommentByIdCommand(int Id) : IRequest;

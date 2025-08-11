using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Commands.TicketComments;
public record UpdateTicketCommentCommand(
    [Required] string Description,
    int Id
) : IRequest;

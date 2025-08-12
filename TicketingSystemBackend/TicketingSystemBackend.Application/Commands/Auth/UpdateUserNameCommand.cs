using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Commands.Auth;
public record UpdateUserNameCommand(
    [Required] string CurrentUserName,
    [Required] string NewUserName
) : IRequest;
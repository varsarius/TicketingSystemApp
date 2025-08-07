using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;

namespace TicketingSystemBackend.Application.Queries.Auth;
public record LoginUserQuery(
    [Required] string Email,
    [Required] string Password
) : IRequest<LoginResponse>;
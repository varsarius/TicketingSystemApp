using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;

namespace TicketingSystemBackend.Application.Commands.Auth;
public record RefreshTokenCommand(string RefreshToken) : IRequest<AuthResponseDto>;

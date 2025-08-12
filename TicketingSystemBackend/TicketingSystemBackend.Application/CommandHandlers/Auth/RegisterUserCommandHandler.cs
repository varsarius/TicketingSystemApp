using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Commands.Auth;
using TicketingSystemBackend.Application.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketingSystemBackend.Application.CommandHandlers.Auth;
public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IAuthRepository _authRepository;

    public RegisterUserCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _authRepository.RegisterUserAsync(
            request.Email,
            request.UserName,
            request.Password,
            cancellationToken
        );
    }
}

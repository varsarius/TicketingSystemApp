using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Auth;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Auth;
public class UpdateUserNameCommandHandler : IRequestHandler<UpdateUserNameCommand>
{
    private readonly IAuthRepository _authRepository;

    public UpdateUserNameCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public async Task Handle(UpdateUserNameCommand request, CancellationToken cancellationToken)
    {
        await _authRepository.UpdateUserNameAsync(request.CurrentUserName, request.NewUserName, cancellationToken);
    }
}

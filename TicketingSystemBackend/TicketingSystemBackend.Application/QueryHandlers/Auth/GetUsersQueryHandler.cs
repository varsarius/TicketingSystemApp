using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.DTOs.Auth;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.Auth;

namespace TicketingSystemBackend.Application.QueryHandlers.Auth;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IAuthRepository _authRepository;

    public GetUsersQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }


    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.GetAllUsersWithRolesAsync();
    }
}
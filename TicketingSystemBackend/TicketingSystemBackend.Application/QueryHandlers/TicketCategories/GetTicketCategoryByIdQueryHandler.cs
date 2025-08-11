using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Application.Queries.TicketCategories;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.QueryHandlers.TicketCategories;
public class GetTicketCategoryByIdQueryHandler : IRequestHandler<GetTicketCategoryByIdQuery, TicketCategory>
{
    private readonly ITicketCategoryRepository _repository;

    public GetTicketCategoryByIdQueryHandler(ITicketCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<TicketCategory> Handle(GetTicketCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}

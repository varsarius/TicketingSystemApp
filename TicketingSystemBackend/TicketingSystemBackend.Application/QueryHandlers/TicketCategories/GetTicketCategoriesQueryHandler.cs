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
public class GetTicketCategoriesQueryHandler : IRequestHandler<GetTicketCategoriesQuery, List<TicketCategory>>
{
    private readonly ITicketCategoryRepository _repository;

    public GetTicketCategoriesQueryHandler(ITicketCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TicketCategory>> Handle(GetTicketCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync();
    }
}

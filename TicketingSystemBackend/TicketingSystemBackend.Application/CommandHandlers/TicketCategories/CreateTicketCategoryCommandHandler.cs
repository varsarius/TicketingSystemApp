using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketCategories;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketCategories;
public class CreateTicketCategoryCommandHandler : IRequestHandler<CreateTicketCategoryCommand>
{
    private readonly ITicketCategoryRepository _repository;

    public CreateTicketCategoryCommandHandler(ITicketCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateTicketCategoryCommand request, CancellationToken cancellationToken)
    {
        var ticketCategory = new TicketCategory
        {
            CategoryName = request.CategoryName,
            UpdatedAt = null
        };
        await _repository.CreateAsync(ticketCategory);
    }
}

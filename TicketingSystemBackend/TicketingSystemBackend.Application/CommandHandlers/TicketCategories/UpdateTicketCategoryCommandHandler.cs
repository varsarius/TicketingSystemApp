using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketCategories;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketCategories;
public class UpdateTicketCategoryCommandHandler : IRequestHandler<UpdateTicketCategoryCommand>
{
    private readonly ITicketCategoryRepository _repository;

    public UpdateTicketCategoryCommandHandler(ITicketCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateTicketCategoryCommand request, CancellationToken cancellationToken)
    {
        var ticketCategory = await _repository.GetByIdAsync(request.Id);
        ticketCategory.CategoryName = request.CategoryName;
        ticketCategory.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(ticketCategory);
    }
}

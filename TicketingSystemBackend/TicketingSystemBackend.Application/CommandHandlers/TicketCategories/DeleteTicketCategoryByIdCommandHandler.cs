using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.TicketCategories;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.TicketCategories;
public class DeleteTicketCategoryByIdCommandHandler : IRequestHandler<DeleteTicketCategoryByIdCommand>
{
    private readonly ITicketCategoryRepository _repository;

    public DeleteTicketCategoryByIdCommandHandler(ITicketCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteTicketCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

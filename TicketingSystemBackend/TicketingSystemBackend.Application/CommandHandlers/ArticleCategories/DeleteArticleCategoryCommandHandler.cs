using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class DeleteArticleCategoryCommandHandler : IRequestHandler<DeleteArticleCategoryByIdCommand>
{
    private readonly ArticleCategoryRepository _repository;

    public DeleteArticleCategoryCommandHandler(ArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteArticleCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

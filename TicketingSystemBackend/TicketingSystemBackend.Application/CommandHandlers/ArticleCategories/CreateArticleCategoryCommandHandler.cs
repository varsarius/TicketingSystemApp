using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand>
{
    private readonly ArticleCategoryRepository _repository;

    public CreateArticleCategoryCommandHandler(ArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = new ArticleCategory
        {
            CategoryName = request.CategoryName
        };
        await _repository.CreateAsync(articleCategory);

    }
}

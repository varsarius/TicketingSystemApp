using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Infrastructure.Repositories;
namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class UpdateArticleCategoryCommandHandler : IRequestHandler<UpdateArticleCategoryCommand>
{
    private readonly ArticleCategoryRepository _repository;

    public UpdateArticleCategoryCommandHandler(ArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _repository.GetByIdAsync(request.Id);
        articleCategory.CategoryName = request.CategoryName;
    }
}

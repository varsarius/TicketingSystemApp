using MediatR;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class CreateArticleCategoryCommandHandler : IRequestHandler<CreateArticleCategoryCommand>
{
    private readonly IArticleCategoryRepository _repository;

    public CreateArticleCategoryCommandHandler(IArticleCategoryRepository repository)
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

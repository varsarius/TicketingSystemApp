using MediatR;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Application.Interfaces;
namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class UpdateArticleCategoryCommandHandler : IRequestHandler<UpdateArticleCategoryCommand>
{
    private readonly IArticleCategoryRepository _repository;

    public UpdateArticleCategoryCommandHandler(IArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateArticleCategoryCommand request, CancellationToken cancellationToken)
    {
        var articleCategory = await _repository.GetByIdAsync(request.Id);
        articleCategory.CategoryName = request.CategoryName;
    }
}

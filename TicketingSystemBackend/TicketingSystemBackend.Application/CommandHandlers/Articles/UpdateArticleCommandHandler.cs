using MediatR;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Interfaces;
namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
{
    private readonly IArticleRepository _repository;

    public UpdateArticleCommandHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(request.Id);
        article.Title = request.Title;
        article.Description = request.Description;
        article.ArticleCategoryId = request.ArticleCategoryId;
        article.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(article);
    }

}

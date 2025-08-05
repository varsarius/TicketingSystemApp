using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Infrastructure.Repositories;
namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand>
{
    private readonly ArticleRepository _repository;

    public UpdateArticleCommandHandler(ArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _repository.GetByIdAsync(request.Id);
        article.Title = request.Title;
        article.Description = request.Description;
        article.ArticleCategoryId = request.ArticleCategoryId;
        article.UpdatedAt = DateTime.Now;
        await _repository.UpdateAsync(article);
    }

}

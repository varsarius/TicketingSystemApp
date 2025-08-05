using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Domain.Entities;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
{
    private readonly ArticleRepository _repository;

    public CreateArticleCommandHandler(ArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = new Article
        {
            Title = request.Title,
            Description = request.Description,
            ArticleCategoryId = request.ArticleCategoryId,
            UserId = request.UserId,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
        await _repository.CreateAsync(article);
    }
}

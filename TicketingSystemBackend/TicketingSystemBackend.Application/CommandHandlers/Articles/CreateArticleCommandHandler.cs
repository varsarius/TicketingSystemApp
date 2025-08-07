using MediatR;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;

namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand>
{
    private readonly IArticleRepository _repository;

    public CreateArticleCommandHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var article = new Article
        {
            Title = request.Title,
            Description = request.Description,
            CategoryId = request.CategoryId,
            UserId = request.UserId,
            CreatedAt = DateTime.Now,//REMOVE: this is not needed!
            UpdatedAt = null
        };
        await _repository.CreateAsync(article);
    }
}

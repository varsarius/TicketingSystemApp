using MediatR;
using TicketingSystemBackend.Application.Commands.ArticleCategories;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.ArticleCategories;
public class DeleteArticleCategoryByIdCommandHandler : IRequestHandler<DeleteArticleCategoryByIdCommand>
{
    private readonly IArticleCategoryRepository _repository;

    public DeleteArticleCategoryByIdCommandHandler(IArticleCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteArticleCategoryByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

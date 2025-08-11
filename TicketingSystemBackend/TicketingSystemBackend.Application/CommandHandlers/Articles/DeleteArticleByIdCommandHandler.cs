using MediatR;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class DeleteArticleByIdCommandHandler : IRequestHandler<DeleteArticleByIdCommand>
{
    private readonly IArticleRepository _repository;

    public DeleteArticleByIdCommandHandler(IArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

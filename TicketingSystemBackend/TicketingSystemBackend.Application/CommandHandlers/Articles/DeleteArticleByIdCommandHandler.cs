using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Infrastructure.Repositories;

namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class DeleteArticleByIdCommandHandler : IRequestHandler<DeleteArticleByIdCommand>
{
    private readonly ArticleRepository _repository;

    public DeleteArticleByIdCommandHandler(ArticleRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteArticleByIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteByIdAsync(request.Id);
    }
}

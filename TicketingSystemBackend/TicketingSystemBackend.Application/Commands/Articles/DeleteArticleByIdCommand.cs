using MediatR;

namespace TicketingSystemBackend.Application.Commands.Articles;

public record DeleteArticleByIdCommand(int Id) : IRequest;

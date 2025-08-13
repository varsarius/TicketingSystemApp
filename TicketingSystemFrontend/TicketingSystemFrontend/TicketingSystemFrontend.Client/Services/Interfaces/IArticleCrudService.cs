using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests.Commands.Articles;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleCrudService : ICrudService<ArticleDto, CreateArticleCommand, UpdateArticleCommand>
{

}

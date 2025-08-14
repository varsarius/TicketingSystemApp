using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleService : ICrudService<ArticleDto, ArticleCreateRequest, ArticleUpdateRequest>
{

}
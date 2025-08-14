using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleCategoryService : ICrudService<ArticleCategoryDto, ArticleCategoryCreateRequest, ArticleCategoryUpdateRequest>
{

}



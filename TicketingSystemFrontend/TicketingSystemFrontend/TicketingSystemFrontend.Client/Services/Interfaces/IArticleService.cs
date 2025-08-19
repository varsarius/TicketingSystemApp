using Microsoft.AspNetCore.Components.Forms;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Extensions;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleService : ICrudService<ArticleDto, ArticleCreateRequest, ArticleUpdateRequest>
    , IFileEntityService
{

}
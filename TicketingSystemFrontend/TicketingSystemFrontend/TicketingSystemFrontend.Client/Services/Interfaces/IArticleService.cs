using Microsoft.AspNetCore.Components.Forms;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IArticleService : ICrudService<ArticleDto, ArticleCreateRequest, ArticleUpdateRequest>
{
    Task UploadFilesAsync(int articleId, List<IBrowserFile> files);
    Task<List<ArticleFileDto>> GetFilesAsync(int articleId);
}
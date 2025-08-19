using Microsoft.AspNetCore.Components.Forms;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services.Extensions;

public interface IFileEntityService
{
    IFileService FileService { get; }
}
public static class FileEntityServiceExtensions
{
    public static Task UploadFilesAsync(this IFileEntityService service, int articleId, List<IBrowserFile> files)
        => service.FileService.UploadAsync(articleId, files, "articles");

    public static Task<List<FileDto>> GetFilesAsync(this IFileEntityService service, int articleId)
        => service.FileService.GetFilesAsync(articleId, "articles");

    public static Task<byte[]> DownloadFileAsync(this IFileEntityService service, int articleId, int fileId)
        => service.FileService.DownloadAsync(articleId, fileId, "articles");

    public static Task DeleteFileAsync(this IFileEntityService service, int articleId, int fileId)
        => service.FileService.DeleteAsync(articleId, fileId, "articles");
}


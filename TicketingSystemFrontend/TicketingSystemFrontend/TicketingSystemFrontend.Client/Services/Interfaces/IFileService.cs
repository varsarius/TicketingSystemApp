using Microsoft.AspNetCore.Components.Forms;
using TicketingSystemFrontend.Client.DTOs;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IFileService
{
    // entity Article -> entityApiType articles (basically the name of the entity in the endpoint route in controllers (backend))
    Task UploadAsync(int entityId, List<IBrowserFile> files, string entityApiType);
    Task<List<FileDto>> GetFilesAsync(int entityId, string entityApiType);
    Task<byte[]> DownloadAsync(int entityId, int fileId, string entityApiType);
    Task DeleteAsync(int entityId, int fileId, string entityApiType);
}

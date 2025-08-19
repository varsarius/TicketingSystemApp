using Microsoft.AspNetCore.Components.Forms;
using TicketingSystemFrontend.Client.DTOs;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IFileService
{
    Task UploadAsync(int entityId, List<IBrowserFile> files, string entityType);
    Task<List<FileDto>> GetFilesAsync(int entityId, string entityType);
    Task<byte[]> DownloadAsync(int entityId, int fileId, string entityType);
    Task DeleteAsync(int entityId, int fileId, string entityType);
}

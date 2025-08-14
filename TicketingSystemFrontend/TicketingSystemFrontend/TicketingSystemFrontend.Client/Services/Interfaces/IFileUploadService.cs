using Microsoft.AspNetCore.Components.Forms;

namespace TicketingSystemFrontend.Client.Services.Interfaces;

public interface IFileUploadService
{
    Task UploadFilesAsync(int entityId, List<IBrowserFile> files, string entityType);
}

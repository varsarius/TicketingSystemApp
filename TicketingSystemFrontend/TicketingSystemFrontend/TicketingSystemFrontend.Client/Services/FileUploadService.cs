using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class FileUploadService : IFileUploadService
{
    private readonly HttpClient _http;

    public FileUploadService(HttpClient http)
    {
        _http = http;
    }
    public async Task UploadFilesAsync(int entityId, List<IBrowserFile> files, string entityType)
    {
        if (!files.Any()) return;

        using var content = new MultipartFormDataContent();

        foreach (var file in files)
        {
            var streamContent = new StreamContent(file.OpenReadStream(long.MaxValue));
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "files", file.Name);
        }

        var response = await _http.PostAsync($"api/{entityType}/{entityId}/files", content);
        response.EnsureSuccessStatusCode();
    }
}

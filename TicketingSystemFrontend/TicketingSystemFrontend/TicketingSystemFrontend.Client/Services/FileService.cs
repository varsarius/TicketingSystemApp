using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Pages.Articles;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;

public class FileService : IFileService
{
    private readonly HttpClient _http;

    public FileService(HttpClient http)
    {
        _http = http;
    }

    public async Task DeleteAsync(int entityId, int fileId, string entityType)
    {
        var response = await _http.DeleteAsync($"api/{entityType}/{entityId}/files/{fileId}");
        response.EnsureSuccessStatusCode();
    }

    public async Task<byte[]> DownloadAsync(int entityId, int fileId, string entityType)
    {
        var response = await _http.GetAsync($"api/{entityType}/{entityId}/files/{fileId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync();
    }

    public async Task<List<FileDto>> GetFilesAsync(int entityId, string entityType)
    {
        return await _http.GetFromJsonAsync<List<FileDto>>($"api/{entityType}/{entityId}/files")
            ?? new List<FileDto>();
    }

    public async Task UploadAsync(int entityId, List<IBrowserFile> files, string entityType)
    {
        if (!files.Any()) return;

        using var content = new MultipartFormDataContent();

        foreach (var file in files)
        {
            // read each file only once
            await using var fileStream = file.OpenReadStream(50 * 1024 * 1024); // 50 MB max
            var memoryStream = new MemoryStream();
            await fileStream.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            var streamContent = new StreamContent(memoryStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "files", file.Name);
        }

        var response = await _http.PostAsync($"api/{entityType}/{entityId}/files", content);
        response.EnsureSuccessStatusCode();
    }
}

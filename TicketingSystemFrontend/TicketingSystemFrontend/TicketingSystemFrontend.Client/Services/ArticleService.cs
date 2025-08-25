using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TicketingSystemFrontend.Client.Auth;
using TicketingSystemFrontend.Client.DTOs;
using TicketingSystemFrontend.Client.Requests;
using TicketingSystemFrontend.Client.Services.Extensions;
using TicketingSystemFrontend.Client.Services.Interfaces;

namespace TicketingSystemFrontend.Client.Services;
public class ArticleService : IArticleService
{
    private readonly HttpClient _http;
    private readonly CustomAuthProvider _authProvider;

    public IFileService FileService { get; }

    public string EntityApiType => "articles";
    private readonly NavigationManager _nav;

    public ArticleService(HttpClient http, CustomAuthProvider authProvider, NavigationManager nav)
    {
        _nav = nav;
        _http = http; // BaseAddress points to Blazor host
        _authProvider = authProvider;
    }

    private string Api(string action) => $"{_nav.BaseUri}server-articles/{action}";

    public async Task<List<ArticleDto>> GetAllAsync()
        => await _http.GetFromJsonAsync<List<ArticleDto>>(Api("all")) ?? new();

    public async Task<ArticleDto?> GetByIdAsync(int id)
        => await _http.GetFromJsonAsync<ArticleDto>(Api($"get/{id}"));

    public async Task<int?> CreateAsync(ArticleCreateRequest request)
    {
        var authState = await _authProvider.GetAuthenticationStateAsync();
        var userId = Guid.Parse(authState.User.FindFirst("sub")!.Value);
        request.UserId = userId;

        var response = await _http.PostAsJsonAsync(Api("create"), request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<int>();
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var response = await _http.DeleteAsync(Api($"delete/{id}"));
        return response.IsSuccessStatusCode;
    }

    public async Task UpdateAsync(ArticleUpdateRequest request)
    {
        var response = await _http.PutAsJsonAsync(Api($"update/{request.Id}"), request);
        response.EnsureSuccessStatusCode();
    }
}

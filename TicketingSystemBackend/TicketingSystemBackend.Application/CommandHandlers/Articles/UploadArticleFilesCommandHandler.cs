using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Commands.Articles;
using TicketingSystemBackend.Application.Interfaces;
using TicketingSystemBackend.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;


namespace TicketingSystemBackend.Application.CommandHandlers.Articles;
public class UploadArticleFilesCommandHandler : IRequestHandler<UploadArticleFilesCommand>
{
    private readonly IArticleRepository _articleRepository;
    private readonly IArticleFileRepository _articleFileRepository;
    private readonly IWebHostEnvironment _env;

    public UploadArticleFilesCommandHandler(
        IArticleRepository articleRepository,
        IArticleFileRepository articleFileRepository,
        IWebHostEnvironment env)
    {
        _articleRepository = articleRepository;
        _articleFileRepository = articleFileRepository;
        _env = env;
    }


    public async Task Handle(UploadArticleFilesCommand request, CancellationToken cancellationToken)
    {
        // Placeholder logic:

        // 1. Validate article exists
        // 2. Create folder path (e.g., wwwroot/uploads/articles/{articleId}/)
        // 3. Save each file to folder
        // 4. Save file paths in DB via repository

        // TODO: Implement file storage and DB linking logic
        // 1. Validate article exists
        // 1. Validate article exists
        var article = await _articleRepository.GetByIdAsync(request.ArticleId);
        if (article == null)
            throw new Exception("Article not found");

        // 2. Create folder path
        // Use ContentRootPath + "wwwroot" as fallback
        var webRoot = _env.WebRootPath ?? Path.Combine(_env.ContentRootPath, "wwwroot");
        var uploadsFolder = Path.Combine(webRoot, "uploads", "articles", request.ArticleId.ToString());

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // 3. Save each file and create File entities
        foreach (var file in request.Files)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream, cancellationToken);

            // 4. Save File entity and link to Article
            var fileEntity = new Domain.Entities.File
            {
                Path = $"/uploads/articles/{request.ArticleId}/{uniqueFileName}"
            };

            fileEntity.Articles.Add(article); // link to article

            await _articleFileRepository.AddAsync(fileEntity);
        }

        await _articleFileRepository.SaveChangesAsync();

    }
}

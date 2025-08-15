using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IArticleFileRepository
{
    // Add a new File entity
    Task AddAsync(Domain.Entities.File file);

    // Save all pending changes to the database
    Task SaveChangesAsync();

    //GetByArticleIdAsync
    // Optional: get files by ArticleId
    Task<List<Domain.Entities.File>> GetFilesByArticleIdAsync(int articleId);

    Task<Domain.Entities.File?> GetFileByIdAndArticleIdAsync(int fileId, int articleId, CancellationToken cancellationToken);
    //// Optional: get a single file by Id
    //Task<File?> GetByIdAsync(int id);

    //// Optional: delete a file
    //Task DeleteAsync(File file);


}

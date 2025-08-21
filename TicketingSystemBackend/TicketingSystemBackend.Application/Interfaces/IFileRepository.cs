using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystemBackend.Application.Interfaces;
public interface IFileRepository
{
    Task<Domain.Entities.File?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    // Article specific
    Task<Domain.Entities.File?> GetFileByIdAndArticleIdAsync(int fileId, int articleId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.File>> GetFilesByArticleIdAsync(int articleId, CancellationToken cancellationToken = default);

    // Ticket specific
    Task<Domain.Entities.File?> GetFileByIdAndTicketIdAsync(int fileId, int ticketId, CancellationToken cancellationToken = default);
    Task<List<Domain.Entities.File>> GetFilesByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default);

    Task AddAsync(Domain.Entities.File file, CancellationToken cancellationToken = default);
    Task DeleteAsync(Domain.Entities.File file, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}

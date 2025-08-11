using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystemBackend.Application.Interfaces;

namespace TicketingSystemBackend.Infrastructure.Services.Auth;
public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RefreshTokenCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.UtcNow;//03:00
            
            var nextRun = DateTime.UtcNow.Date.AddHours(3); // Today at 03:00 UTC
            

            if (now >= nextRun)
                nextRun = nextRun.AddDays(1); // If already past 03:00, schedule for next day

            var delay = nextRun - now;

            await Task.Delay(delay, stoppingToken);
            // Run the cleanup job
            using (var scope = _serviceProvider.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IAuthRepository>();
                await repo.RemoveExpiredTokensAsync(stoppingToken);
            }
        }
    }
}

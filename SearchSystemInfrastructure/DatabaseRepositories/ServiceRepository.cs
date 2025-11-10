using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SearchSystemApplication.MappingExtensions;
using SearchSystemDomain.Entities;

namespace SearchSystemInfrastructure.DatabaseRepositories;

public class ServiceRepository : IServiceRepository
{
    private readonly SearchSystemDbContext _dbContext;
    private readonly ILogger<ServiceRepository> _logger;

    public ServiceRepository(SearchSystemDbContext dbContext, ILogger<ServiceRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;

    }

    public async Task<List<ServiceInfo>> GetServiceInfosAsync()
    {
        _logger.LogInformation("Repository: Fetching service information.");
        var services = await _dbContext.Services.ToListAsync();
        _logger.LogInformation("Repository: Returned {Count} service information records.", services.Count);
        return services.Select(s => s.ToDomainEntity()).ToList();
    }
}

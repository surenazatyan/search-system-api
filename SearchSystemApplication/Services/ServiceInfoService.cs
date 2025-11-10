using Microsoft.Extensions.Logging;
using SearchSystemDomain.Entities;
using SearchSystemInfrastructure.DatabaseRepositories;

namespace SearchSystemApplication.Services;

public class ServiceInfoService : IServiceInfoService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly ILogger<ServiceInfoService> _logger;

    public ServiceInfoService(IServiceRepository serviceRepository, ILogger<ServiceInfoService> logger)
    {
        _serviceRepository = serviceRepository;
        _logger = logger;
    }


    public async Task<List<ServiceInfo>> GetServiceInfosAsync()
    {
        _logger.LogInformation($"{nameof(ServiceInfoService.GetServiceInfosAsync)} Fetching all services.");
        var services = await _serviceRepository.GetServiceInfosAsync();
        _logger.LogInformation($"{nameof(ServiceInfoService.GetServiceInfosAsync)} Fetched {services?.Count ?? 0} services.");
        return services;
    }

}
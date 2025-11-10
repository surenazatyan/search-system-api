using SearchSystemDomain.Entities;

namespace SearchSystemInfrastructure.DatabaseRepositories;

public interface IServiceRepository
{
    Task<List<ServiceInfo>> GetServiceInfosAsync();
}
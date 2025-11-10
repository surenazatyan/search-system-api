using SearchSystemDomain.Entities;

namespace SearchSystemApplication.Services;
public interface IServiceInfoService
{
    Task<List<ServiceInfo>> GetServiceInfosAsync();
}
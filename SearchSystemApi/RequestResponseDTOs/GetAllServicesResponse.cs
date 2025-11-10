using SearchSystemDomain.Entities;

namespace SearchSystemApi.RequestResponseDTOs;
public class GetAllServicesResponse
{
    public List<ServiceInfo> Services { get; set; } = new List<ServiceInfo>();
}

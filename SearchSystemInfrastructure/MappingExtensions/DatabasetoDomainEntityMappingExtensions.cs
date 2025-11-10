using SearchSystemDomain.Entities;
using SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

namespace SearchSystemApplication.MappingExtensions;

public static class DatabasetoDomainEntityMappingExtensions
{
    public static PositionInfo ToDomainEntity(this PositionInfoDatabaseDto source)
    {
        return new PositionInfo(source.Lat, source.Lng);
    }

    public static ServiceInfo ToDomainEntity(this ServiceInfoDatabaseDto source)
    {
        return new ServiceInfo
        {
            Id = source.Id,
            Name = source.Name,
            Position = source.Position.ToDomainEntity()
        };
    }
}
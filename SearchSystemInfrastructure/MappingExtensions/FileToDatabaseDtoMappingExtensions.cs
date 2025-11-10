using SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

namespace SearchSystemApplication.MappingExtensions;

public static class FileToDatabaseDtoMappingExtensions
{
    public static PositionInfoDatabaseDto ToDatabaseDto(this PositionInfoFileDto source)
    {
        return new PositionInfoDatabaseDto(source.Lat, source.Lng);
    }

    public static ServiceInfoDatabaseDto ToDatabaseDto(this ServiceInfoFileDto source)
    {
        return new ServiceInfoDatabaseDto
        {
            Id = source.Id,
            Name = source.Name,
            Position = source.Position.ToDatabaseDto()
        };
    }
}
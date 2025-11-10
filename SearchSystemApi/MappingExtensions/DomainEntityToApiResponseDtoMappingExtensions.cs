using SearchSystemApi.RequestResponseDTOs;
using SearchSystemDomain.Entities;

namespace SearchSystemApplication.MappingExtensions;

public static class DatabasetoDomainEntityMappingExtensions
{
    public static GeoLocation ToResponseDto(this PositionInfo source)
    {
        return new GeoLocation { Lat = source.Lat, Lng = source.Lng };
    }
}
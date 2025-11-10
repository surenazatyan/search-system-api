using System.Text.Json.Serialization;

namespace SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

public sealed class PositionInfoDatabaseDto
{
    [JsonPropertyName("lat")]
    public double Lat { get; init; }

    [JsonPropertyName("lng")]
    public double Lng { get; init; }

    public PositionInfoDatabaseDto() { }

    public PositionInfoDatabaseDto(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
    }
}
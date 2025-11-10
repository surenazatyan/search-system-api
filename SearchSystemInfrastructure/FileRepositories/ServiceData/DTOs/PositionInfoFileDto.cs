using System.Text.Json.Serialization;

namespace SearchSystemInfrastructure.FileRepositories.ServicesData.DTOs;

public sealed class PositionInfoFileDto
{
    [JsonPropertyName("lat")]
    public double Lat { get; init; }

    [JsonPropertyName("lng")]
    public double Lng { get; init; }

    public PositionInfoFileDto() { }

    public PositionInfoFileDto(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
    }
}
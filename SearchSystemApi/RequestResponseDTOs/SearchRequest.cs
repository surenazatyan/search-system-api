namespace SearchSystemApi.RequestResponseDTOs;
public class SearchRequest
{
    public string ServiceName { get; set; }
    public GeoLocation Position { get; set; }
}

public class GeoLocation
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

namespace SearchSystemDomain.Entities;

public sealed class PositionInfo
{
    public double Lat { get; set; }

    public double Lng { get; set; }

    public PositionInfo() { }

    public PositionInfo(double lat, double lng)
    {
        Lat = lat;
        Lng = lng;
    }
}

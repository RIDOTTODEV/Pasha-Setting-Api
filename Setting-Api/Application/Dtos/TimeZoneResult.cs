namespace Setting_Api.Application.Dtos;

public class TimeZoneResult
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public List<Zone> Zones { get; set; } = [];
}
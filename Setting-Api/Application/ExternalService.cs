using System.Text.Json;
using System.Web;
using Setting_Api.Application.Dtos;

namespace Setting_Api.Application;

public interface IExternalService
{
    Task<int> GetTimeZone(string zone = "Europe/Athens");
}
public class ExternalService(HttpClient httpClient): IExternalService
{
    private HttpClient _httpClient = httpClient;
    
    public async Task<int> GetTimeZone(string zone)
    {
        var url = new UriBuilder(_httpClient.BaseAddress + "list-time-zone");
        var query = HttpUtility.ParseQueryString(url.Query);
        query["key"] = "FJSZ3IEK6RDK"; 
        query["format"] = "json";
        query["zone"] = zone;
        query["fields"] = "zoneName,gmtOffset";
        url.Query = query.ToString();

        var response = await _httpClient.GetAsync(url.ToString());

        if (!response.IsSuccessStatusCode) return 0;
        var content = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<TimeZoneResult>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        var offSet = data.Zones.First().GmtOffset / 3600;
        return offSet;

    }
}

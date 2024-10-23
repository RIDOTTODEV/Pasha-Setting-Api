namespace Setting_Api.Model.Entity;

public class Setting
{
    public int Id { get; set; }
    public int CasinoId { get; set; }
    public int GamingDateTurnHour { get; set; }
    public int GamingDateTurnMinute { get; set; }
    public int TimeZone { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace Setting_Api.Model.Enums;

public enum CasinoTypes
{
    [Display(Name = "Grand Pasha Kyrenia")]
    Girne = 1,
    [Display(Name = "Grand Pasha Nicosia")]
    Lefkosa = 2,
    [Display(Name = "Grand Pasha Saray")] 
    Saray = 3,
    [Display(Name = "Development")]
    Dev = 4
}
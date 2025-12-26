using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Controls;

public class MonthSelectViewModel
{
    [Required(ErrorMessage = "Выберите месяц")]
    public string? Month { get; set; }
}

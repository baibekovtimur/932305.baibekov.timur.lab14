using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Controls;

public class ListBoxViewModel
{
    [Required(ErrorMessage = "Выберите месяц")]
    public string? Month { get; set; }
}

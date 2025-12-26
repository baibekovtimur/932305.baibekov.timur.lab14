using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Controls;

public class RadioViewModel
{
    [Required(ErrorMessage = "Выберите вариант")]
    public string? Choice { get; set; }
}

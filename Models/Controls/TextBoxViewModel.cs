using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Controls;

public class TextBoxViewModel
{
    [Required(ErrorMessage = "Введите текст")]
    [StringLength(100)]
    public string? Text { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Controls;

public class TextAreaViewModel
{
    [Required(ErrorMessage = "Введите текст")]
    [StringLength(500)]
    public string? Text { get; set; }
}

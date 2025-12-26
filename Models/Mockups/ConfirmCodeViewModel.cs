using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Mockups;

public class ConfirmCodeViewModel
{
    [Required(ErrorMessage = "Введите код")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Код должен содержать 6 цифр")]
    [RegularExpression(@"^\d{6}$", ErrorMessage = "Код должен содержать 6 цифр")]
    [Display(Name = "Code")]
    public string? Code { get; set; }
}

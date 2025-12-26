using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Mockups;

public class ForgotPasswordViewModel
{
    [Required(ErrorMessage = "Введите email")]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}

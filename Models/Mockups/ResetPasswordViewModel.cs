using System.ComponentModel.DataAnnotations;

namespace Backend4.Models.Mockups;

public class ResetPasswordViewModel
{
    [Required]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Введите новый пароль")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не короче 6 символов")]
    [DataType(DataType.Password)]
    [Display(Name = "New password")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Подтвердите пароль")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string? ConfirmPassword { get; set; }
}

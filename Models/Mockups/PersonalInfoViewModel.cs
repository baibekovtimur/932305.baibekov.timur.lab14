using System.ComponentModel.DataAnnotations;
using Backend4.Validation;

namespace Backend4.Models.Mockups;

public class PersonalInfoViewModel
{
    [Required(ErrorMessage = "Введите имя")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов")]
    [Display(Name = "First name")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Введите фамилию")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Фамилия должна быть от 2 до 50 символов")]
    [Display(Name = "Last name")]
    public string? LastName { get; set; }

    [Range(1, 31, ErrorMessage = "Выберите день")]
    public int BirthDay { get; set; }

    [Range(1, 12, ErrorMessage = "Выберите месяц")]
    public int BirthMonth { get; set; }

    [Range(1900, 2100, ErrorMessage = "Выберите год")]
    public int BirthYear { get; set; }

    [Required(ErrorMessage = "Укажите дату рождения")]
    [MinAge(14, ErrorMessage = "Регистрация доступна с 14 лет")]
    [Display(Name = "Birthday")]
    public DateTime? BirthDate
    {
        get
        {
            if (BirthDay <= 0 || BirthMonth <= 0 || BirthYear <= 0) return null;
            if (!DateTime.TryParse($"{BirthYear:D4}-{BirthMonth:D2}-{BirthDay:D2}", out var dt)) return null;
            return dt;
        }
    }

    [Required(ErrorMessage = "Выберите пол")]
    [Display(Name = "Gender")]
    public string? Gender { get; set; } // "Male" / "Female"
}

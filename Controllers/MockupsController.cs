using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Backend4.Models.Mockups;
using Backend4.Infrastructure;

namespace Backend4.Controllers;

public class MockupsController : Controller
{
    private const string SignUpPersonalKey = "SignUp.Personal";
    private const string ResetEmailKey = "Reset.Email";
    private const string ResetCodeKey = "Reset.Code";
    private const string ResetVerifiedKey = "Reset.Verified";

    [HttpGet]
    public IActionResult Index() => View();

    // -----------------------
    // Sign up (2 steps)
    // -----------------------

    [HttpGet]
    public IActionResult SignUp() => View(new PersonalInfoViewModel());

    [HttpPost]
    public IActionResult SignUp(PersonalInfoViewModel model)
    {
        // 1) declarative validation
        if (!ModelState.IsValid) return View(model);

        // 2) controller-level validation example
        if (string.Equals(model.FirstName?.Trim(), model.LastName?.Trim(), StringComparison.OrdinalIgnoreCase))
            ModelState.AddModelError(string.Empty, "Имя и фамилия не должны совпадать");

        if (!ModelState.IsValid) return View(model);

        HttpContext.Session.SetJson(SignUpPersonalKey, model);

        return RedirectToAction(nameof(SignUpCredentials));
    }

    [HttpGet]
    public IActionResult SignUpCredentials()
    {
        var personal = HttpContext.Session.GetJson<PersonalInfoViewModel>(SignUpPersonalKey);
        if (personal is null) return RedirectToAction(nameof(SignUp));

        return View(new CredentialsViewModel());
    }

    [HttpPost]
    public IActionResult SignUpCredentials(CredentialsViewModel model)
    {
        var personal = HttpContext.Session.GetJson<PersonalInfoViewModel>(SignUpPersonalKey);
        if (personal is null)
        {
            ModelState.AddModelError(string.Empty, "Сессия регистрации истекла. Начните заново.");
            return View(model);
        }

        if (!ModelState.IsValid) return View(model);

        var result = new SignUpResultViewModel
        {
            FirstName = personal.FirstName,
            LastName = personal.LastName,
            BirthDate = personal.BirthDate,
            Gender = personal.Gender,
            Email = model.Email,
            Password = model.Password
        };

        HttpContext.Session.Remove(SignUpPersonalKey);

        return View("SignUpResult", result);
    }

    // -----------------------
    // Forgot / Reset password (3 screens)
    // -----------------------

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        HttpContext.Session.RemoveMany(ResetEmailKey, ResetCodeKey, ResetVerifiedKey);
        return View(new ForgotPasswordViewModel());
    }

    [HttpPost]
    public IActionResult ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // имитация отправки кода на почту
        var code = Random.Shared.Next(0, 1_000_000).ToString("D6");

        HttpContext.Session.SetString(ResetEmailKey, model.Email!);
        HttpContext.Session.SetString(ResetCodeKey, code);
        HttpContext.Session.SetString(ResetVerifiedKey, "0");

        return RedirectToAction(nameof(ForgotPasswordConfirm));
    }

    [HttpGet]
    public IActionResult ForgotPasswordConfirm()
    {
        var email = HttpContext.Session.GetString(ResetEmailKey);
        var code = HttpContext.Session.GetString(ResetCodeKey);

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(code))
            return RedirectToAction(nameof(ForgotPassword));

        ViewBag.Email = email;
        ViewBag.SentCode = code; // для учебного проекта выводим код на экран

        return View(new ConfirmCodeViewModel());
    }

    [HttpPost]
    public IActionResult ForgotPasswordConfirm(ConfirmCodeViewModel model)
    {
        var expected = HttpContext.Session.GetString(ResetCodeKey);

        if (expected is null) return RedirectToAction(nameof(ForgotPassword));

        if (!ModelState.IsValid) return RedirectToAction(nameof(ForgotPasswordConfirm));

        if (!string.Equals(model.Code, expected, StringComparison.Ordinal))
        {
            TempData["Error"] = "Неверный код. Попробуйте ещё раз.";
            return RedirectToAction(nameof(ForgotPasswordConfirm));
        }

        HttpContext.Session.SetString(ResetVerifiedKey, "1");
        return RedirectToAction(nameof(ResetPassword));
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        var email = HttpContext.Session.GetString(ResetEmailKey);
        var verified = HttpContext.Session.GetString(ResetVerifiedKey);

        if (email is null || verified != "1")
            return RedirectToAction(nameof(ForgotPassword));

        return View(new ResetPasswordViewModel { Email = email });
    }

    [HttpPost]
    public IActionResult ResetPassword(ResetPasswordViewModel model)
    {
        var email = HttpContext.Session.GetString(ResetEmailKey);
        var verified = HttpContext.Session.GetString(ResetVerifiedKey);

        if (email is null || verified != "1")
            return RedirectToAction(nameof(ForgotPassword));

        // Email приходит скрытым полем, но доверяем сессии
        model.Email = email;

        if (!ModelState.IsValid) return View(model);

        // имитация сохранения нового пароля
        HttpContext.Session.RemoveMany(ResetEmailKey, ResetCodeKey, ResetVerifiedKey);

        return View("ResetPasswordResult", new ResetPasswordResultViewModel { Email = email, Success = true });
    }
}

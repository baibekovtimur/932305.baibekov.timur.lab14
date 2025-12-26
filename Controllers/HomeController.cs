using Microsoft.AspNetCore.Mvc;

namespace Backend4.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}

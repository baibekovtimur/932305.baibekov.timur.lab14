using Microsoft.AspNetCore.Mvc;
using Backend4.Models.Controls;

namespace Backend4.Controllers;

public class ControlsController : Controller
{
    private static readonly string[] Months =
    {
        "January","February","March","April","May","June",
        "July","August","September","October","November","December"
    };

    [HttpGet]
    public IActionResult Index() => View();

    [HttpGet]
    public IActionResult TextBox() => View(new TextBoxViewModel());

    [HttpPost]
    public IActionResult TextBox(TextBoxViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        ViewBag.Result = model.Text;
        return View(model);
    }

    [HttpGet]
    public IActionResult TextArea() => View(new TextAreaViewModel());

    [HttpPost]
    public IActionResult TextArea(TextAreaViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        ViewBag.Result = model.Text;
        return View(model);
    }

    [HttpGet]
    public IActionResult CheckBox() => View(new CheckBoxViewModel());

    [HttpPost]
    public IActionResult CheckBox(CheckBoxViewModel model) => View(model);

    [HttpGet]
    public IActionResult Radio() => View(new RadioViewModel());

    [HttpPost]
    public IActionResult Radio(RadioViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        return View(model);
    }

    [HttpGet]
    public IActionResult DropDownList()
    {
        ViewBag.Months = Months;
        return View(new MonthSelectViewModel { Month = "January" });
    }

    [HttpPost]
    public IActionResult DropDownList(MonthSelectViewModel model)
    {
        ViewBag.Months = Months;
        if (!ModelState.IsValid) return View(model);
        return View(model);
    }

    [HttpGet]
    public IActionResult ListBox()
    {
        ViewBag.Months = Months;
        return View(new ListBoxViewModel { Month = "April" });
    }

    [HttpPost]
    public IActionResult ListBox(ListBoxViewModel model)
    {
        ViewBag.Months = Months;
        if (!ModelState.IsValid) return View(model);
        return View(model);
    }
}

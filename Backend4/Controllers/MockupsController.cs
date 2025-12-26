using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend4.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace Backend4.Controllers
{
    public class MockupsController : Controller
    {
        Random rnd = new Random();

        public IActionResult Index()
        {
            return View();
        }
        [AcceptVerbs("GET", "POST")]
        public IActionResult CheckEmail(string email)
        {
            if (IdentityMap.Get().ContainsKey(email))
                return Json(false);
            return Json(true);
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(Auth a)
        {
            if (ModelState["FirstName"].ValidationState == ModelValidationState.Valid &
                ModelState["LastName"].ValidationState == ModelValidationState.Valid &
                ModelState["Gender"].ValidationState == ModelValidationState.Valid)
                return RedirectToAction("SignUp2", a);
            return View();
        }
        [HttpGet]
        public IActionResult SignUp2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp2(Auth a)
        {

            if (ModelState["Email"].ValidationState == ModelValidationState.Valid &
                ModelState["Password"].ValidationState == ModelValidationState.Valid &
                ModelState["ConfirmPassword"].ValidationState == ModelValidationState.Valid)
            {
                if (IdentityMap.Get().ContainsKey(a.Email)) return View(a);
                    IdentityMap.Get().Add(a.Email, a);
                return View("Result", a);
            }
            return View();
        }
        [HttpGet]
        public IActionResult Reset2()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reset2(Auth a, string myTextbox)
        {

            if (a.Code == myTextbox)
            {
                return RedirectToAction("Reset3", a);
            }
            ViewBag.Check = "Wrong code";
            return View(a);
        }
        [HttpGet]
        public IActionResult Reset3()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reset3(Auth a)
        {

            if (ModelState["Password"].ValidationState == ModelValidationState.Valid &
                ModelState["ConfirmPassword"].ValidationState == ModelValidationState.Valid)
                return View("Result2");
            else View(a);

            return View();
        }
        [HttpGet]
        public IActionResult Reset()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Reset(string Email, string action)
        {
            Auth a;
            if (Email==null)
            {
                ViewBag.Code = "Вы не ввели email";
                return View();
            }
            if (IdentityMap.Get().TryGetValue(Email, out a))
              { if (action == "Send me a code")
                {
                    string b= Convert.ToString(rnd.Next(0, 10)) + Convert.ToString(rnd.Next(0, 10)) + Convert.ToString(rnd.Next(0, 10)) + Convert.ToString(rnd.Next(0, 10));
                    ViewBag.Code = b;
                    IdentityMap.Get()[Email].Code = b;
                    return View();
                }
                else  return RedirectToAction("Reset2", a);
            }
            ViewBag.Code = "Вы не зарегистрированы";
            return View();
        }
    }
}

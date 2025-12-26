using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend4.Controllers
{
    public class Controls : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckBox(bool isSelected)
        {
            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[1];
                ViewBag.Field = "CheckBox";
                ViewBag.Name = "isSelected";
                ViewBag.myArray[0] = Convert.ToString(isSelected);
                return View("Result");
            }
            else return View();

        }

        public IActionResult TextBox(string myTextbox)
        {

            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[1];
                ViewBag.Field = "TextBox";
                ViewBag.Name = "Text";
                ViewBag.myArray[0] = myTextbox;
                return View("Result");
            }
            else return View();
        }
        public IActionResult TextArea(string myTextArea)
        {
            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[1];
                ViewBag.Field = "TextArea";
                ViewBag.Name = "Text";
                ViewBag.myArray[0] = myTextArea;
                return View("Result");
            }
            else return View();
        }
        public IActionResult Radio(string myRadiobutton)
        {
            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[1];
                ViewBag.Field = "Radio";
                ViewBag.Name = "Month";
                ViewBag.myArray[0] = myRadiobutton;
                return View("Result");
            }
            else return View();
        }
        public IActionResult DropDownList(string myList)
        {
            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[1];
                ViewBag.Field = "DropDownList";
                ViewBag.Name = "Month";
                ViewBag.myArray[0] = myList;
                return View("Result");
            }
            else return View();
        }
        public IActionResult List(string[] myList)
        {
           
            if (Request.Method == "POST")
            {
                ViewBag.MyArray = new string[myList.Length];
                ViewBag.Field = "List";
                ViewBag.Name = "Months";
                int i=0;
                foreach (string list in myList)
                {
                    ViewBag.myArray[i] = list;
                    i++;
                }
                return View("Result");

            }
            else return View();
        }
    }
}

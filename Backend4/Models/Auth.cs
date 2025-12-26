using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Backend4.Models
{
    public class Auth
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        [Remote(action: "CheckEmail", controller: "Mockups")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан день")]
        public int Day { get; set; }
        [Required(ErrorMessage = "Не указан месяц")]
        public string Month { get; set; }
        [Required(ErrorMessage = "Не указан год")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Не указан пол")]
        public string Gender { get; set; }
        public bool Remember { get; set; }
        public string Code { get; set; }
    }



    public class IdentityMap
    {
        private static readonly IdentityMap Instance = new IdentityMap();
        private  Dictionary<string, Auth> accounts = new Dictionary<string, Auth>();
        private IdentityMap() { }
        public static int alls;
        public static int rights;
        public static void Up()
        {
            Instance.accounts.Clear();
            alls = 0;
            rights = 0;
        }
        public static void AddAction(Auth account, string email)
        {
            Instance.accounts.Add(email, account);
        }
        public static Dictionary<string, Auth> Get()
        {
            return Instance.accounts;
        }
    }
}

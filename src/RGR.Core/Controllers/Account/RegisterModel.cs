using System.ComponentModel.DataAnnotations;

namespace RGR.Core.Controllers.Account
{
    public class RegisterModel
    {
        public bool EmailOK = false;
        public bool PasswordOK = false;
        public bool PasswordConfirmationOK = false;
        public string ErrorMessage;

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

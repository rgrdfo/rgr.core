using System.Threading.Tasks;
using System.Collections.Generic;
using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RGR.Core.Controllers.Account
{

    public class LoginModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

    //public enum LoginError : byte { NoSuchUser, LoginInvalid, PasswordEmpty, PasswordIncorrect, ConnectionError }
    //public class LoginModel
    //{
    //    private rgrContext db;
    //    public List<LoginError> Errors { get; private set; }

    //    internal readonly string Email;
    //    internal readonly string Password;

    //    public LoginModel(rgrContext DataBase, string Email, string Password)
    //    {
    //        db = DataBase;
    //        Errors = new List<LoginError>();
    //        this.Email = Email;
    //        this.Password = Password;
    //    }

    //    public async Task<bool> CheckLoginPossilblityAsync()
    //    {
    //        Errors.Clear();

    //        //Проверка адреса почты на корректность
    //        if (!Email.Contains("@") || !Email.Contains(".") || Email.Length < 6)
    //            Errors.Add(LoginError.LoginInvalid);

    //        //Проверка адреса на наличие в БД
    //        if (await db.Users.FirstOrDefaultAsync(u => u.Login == Email) == null)
    //            Errors.Add(LoginError.NoSuchUser);

    //        //Пароль не может быть пустым
    //        if (string.IsNullOrEmpty(Password))
    //            Errors.Add(LoginError.PasswordEmpty);
    //        else
    //        {
    //            //Проверка правильности пароля
    //            var hash = PasswordUtils.GenerateMD5PasswordHash(Password);
    //            if (await db.Users.FirstOrDefaultAsync(u => u.PasswordHash == hash) == null)
    //                Errors.Add(LoginError.PasswordIncorrect);
    //        }

    //        //Если список ошибок не пуст, авторизоваться нельзя
    //        if (Errors.Count > 0)
    //            return false;

    //        //Можно логиниться!
    //        return true;
    //    }
    //}

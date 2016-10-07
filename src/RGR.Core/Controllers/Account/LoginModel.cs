using System.Threading.Tasks;
using System.Collections.Generic;
using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Controllers.Account
{
    public enum LoginError : byte { NoSuchUser, LoginInvalid, PasswordEmpty, PasswordIncorrect, ConnectionError }
    public class LoginModel
    {
        private rgrContext db;
        public List<LoginError> Errors { get; private set; }

        public LoginModel(rgrContext DataBase)
        {
            db = DataBase;
            Errors = new List<LoginError>();
        }

        public async Task<bool> CheckLoginPossilblityAsync(string email, string password)
        {
            Errors.Clear();

            //Проверка адреса почты на корректность
            if (!email.Contains("@") || !email.Contains(".") || email.Length < 6)
                Errors.Add(LoginError.LoginInvalid);

            //Проверка адреса на наличие в БД
            if (await db.Users.FirstOrDefaultAsync(u => u.Login == email) == null)
                Errors.Add(LoginError.NoSuchUser);

            //Пароль не может быть пустым
            if (string.IsNullOrEmpty(password))
                Errors.Add(LoginError.PasswordEmpty);
            else
            {
                //Проверка правильности пароля
                var hash = PasswordUtils.GenerateMD5PasswordHash(password);
                if (await db.Users.FirstOrDefaultAsync(u => u.PasswordHash == hash) == null)
                    Errors.Add(LoginError.PasswordIncorrect);
            }

            //Если список ошибок не пуст, авторизоваться нельзя
            if (Errors.Count > 0)
                return false;

            //Можно логиниться!
            return true;
        }
    }
}

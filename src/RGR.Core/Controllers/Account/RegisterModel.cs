using System.Threading.Tasks;
using System.Collections.Generic;
using RGR.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RGR.Core.Controllers.Account
{
    public enum RegError : byte { LoginInvalid, UserExists, ShortPassword, PasswordsDontMatch }

    /// <summary>
    /// Регистрация нового пользователя. При объявлении экземпляра следует указать БД и переменную класса Users, которая будет доавлена в базу
    /// После конструкции экземпляра следует вызвать метод TryRegisterAsync В случае отсутствия ошибок регистрации переменной User будет присвоено имя,
    /// переданное в метод, а так же хэш пароля, на основе аргумента password
    /// </summary>
    public class RegisterModel
    {
        private rgrContext db;
        
        public List<RegError> Errors { get; private set; }
        public Users User;

        /// <summary>
        /// Инициализирует новый экземпляр класса RegisterModel. Если в параметре User передать null, переменной будет присвоено значение "new Users()"
        /// </summary>
        /// <param name="DataBase"></param>
        /// <param name="User"></param>
        public RegisterModel(rgrContext DataBase, Users User = null)
        {
            db = DataBase;
            this.User = User ?? new Users();
            Errors = new List<RegError>();
        }

        /// <summary>
        /// Попытка добавления нового пользователя в БД
        /// </summary>
        /// <param name="email">Логин пользователя (в нашем случае - его почта)</param>
        /// <param name="password">Пароль</param>
        /// <param name="confirmation">Подтверждение пароля</param>
        /// <returns>Результат попытки. Если возвращается true - пользователь добавлен в БД. В случае false, следует обратиться к списку ошибок регистрации Errors</returns>
        public async Task<bool> TryRegisterAsync(string email, string password, string confirmation)
        {
            Errors.Clear();

            //Проверка адреса почты на корректность
            if (!email.Contains("@") || !email.Contains(".") || email.Length < 6)
                Errors.Add(RegError.LoginInvalid);

            //Проверка адреса на наличие в БД
            if (await db.Users.FirstOrDefaultAsync(u => u.Login == email) != null)
                Errors.Add(RegError.UserExists);

            //Проверка длины пароля
            if (password.Length < 6)
                Errors.Add(RegError.ShortPassword);

            //Проверка подтверждения пароля
            if (password != confirmation)
                Errors.Add(RegError.PasswordsDontMatch);

            //Если список ошибок не пуст, регистрация не производится
            if (Errors.Count > 0)
                return false;

            //Если ошибок нет, пользователю заполняются обязательные поля
            User.Login = email;
            User.PasswordHash = PasswordUtils.GenerateMD5PasswordHash(password);

            //Добавление нвого пользователя
            db.Users.Add(User);

            //Ура!
            return true;
        }
    }
}

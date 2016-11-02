//using Danko.TextJobs;
using Eastwing.Parser;
using RGR.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.Core.Common
{
    /// <summary>
    /// Назначение файла. Используется при помещении файла в хранилище
    /// </summary>
    public enum FilePurpose : byte { UserPhoto, ObjectPhoto, CompanyLogo };

    /// <summary>
    /// Инкапсулирует методы для работы с файловым хранилищем
    /// </summary>
    public static class StorageUtils
    {
        /// <summary>
        /// Получение строки, содержащей путь до файла для использования в представлениях
        /// </summary>
        /// <param name="Id">Индекс файла в БД</param>
        /// <param name="Files">Таблица файлов</param>
        /// <returns></returns>
        public static string GetFileViewPath(long Id, List<StoredFiles> Files)
        {
            var file = Files.FirstOrDefault(f => f.Id == Id);
            if (file == null)
                return null;

            //Замена слэшей для записей, оставшихся от старого сайта
            var path = file.ServerFilename.Replace('\\','/');
            return $"/images/{path}";
        }
        /// <summary>
        /// Получение строки, содержащей путь до файла для использования в представлениях
        /// </summary>
        /// <param name="URI">Ссылка на файл</param>
        /// <param name="db">Указание на БД</param>
        /// <returns></returns>
        public static string GetFileViewPath(string URI, rgrContext db)
        {
            var parser = new Parser()
            {
                Letters = "",
                Separators = "",
                Brackets = ""
            };
            long Id;
            //Разбор URI и попытка вычленить Id файла
            var lexemes = parser.Parse(URI);
            var strId = lexemes.FirstOrDefault(l => l.Category == Category.Integer).Lexeme;
            if (strId == null)
                return null;

            Id = long.Parse(strId);
            return GetFileViewPath(Id, db.StoredFiles.ToList());
        }

        /// <summary>
        /// Сохраняет файл на диск и создёт запись в БД
        /// </summary>
        /// <param name="Content">Содержимое файла</param>
        /// <param name="FileName">Исхолное имя файла</param>
        /// <param name="Purpose">Назначение файла. Определяет его расположение на диске</param>
        /// <param name="CreatorId">Индекс БД пользователя, добавляющего файл</param>
        /// <param name="wwwroot">Путь до папки wwwroot</param>
        /// <param name="db">Контекст БД</param>
        internal static void Store(byte[] Content, string FileName, FilePurpose Purpose, long CreatorId, string wwwroot, rgrContext db)
        {
            //Извлечение короткого имени и расширения файла
            var parts = FileName.Split('.');
            var ext  = (parts.Length > 1) ? parts.Last() : "";
            var name = Path.GetRandomFileName();

            //Преобразование назначения файла в подкаталог хранения
            string placement = $"{Purpose}s";

            File.WriteAllBytes(Path.Combine(wwwroot, placement, $"{name}.{ext}"), Content);

            StoredFiles StoredFile = MakeEntry(ext, Content.Length, FileName, Path.Combine(placement, $"{name}.{ext}"), CreatorId);
            db.Add(StoredFile);
            db.SaveChanges();
        }


        /// <summary>
        /// Сохраняет файл на диск и создёт запись в БД. Асинхронная версия, пытается создать запись и поместить файл на диск одновременно. Подходит для файлов размером ~10-300 мб.
        /// </summary>
        /// <param name="Content">Содержимое файла</param>
        /// <param name="FileName">Исхолное имя файла</param>
        /// <param name="Purpose">Назначение файла. Определяет его расположение на диске</param>
        /// <param name="CreatorId">Индекс БД пользователя, добавляющего файл</param>
        /// <param name="wwwroot">Путь до папки wwwroot</param>
        /// <param name="db">Контекст БД</param>
        /// <returns></returns>
        internal static async Task StoreAsync(byte[] Content, string FileName, FilePurpose Purpose, long CreatorId, string wwwroot, rgrContext db)
        {
            //Извлечение короткого имени и расширения файла
            var parts = FileName.Split('.');
            var ext = (parts.Length > 1) ? parts.Last() : "";
            var name = Path.GetRandomFileName();

            //Преобразование назначения файла в подкаталог хранения
            string placement = $"{Purpose}s";

            await Task.Run(() =>
                {
                    Task tEntry = Task.Run(async () =>
                    {
                        StoredFiles StoredFile = MakeEntry(ext, Content.Length, FileName, Path.Combine(placement, $"{name}.{ext}"), CreatorId);
                        db.Add(StoredFile);
                        await db.SaveChangesAsync();
                    });

                    Task tPlace = Task.Run(() =>
                    {
                        File.WriteAllBytes(Path.Combine(wwwroot, placement, $"{name}.{ext}"), Content);
                    });

                    Task.WaitAll(tEntry, tPlace);
                });
        }

        private static string GetMIME(string ext)
        {
            switch (ext.ToUpper())
            {
                case "JPG":
                case "JPEG":
                    return "image/jpeg";

                case "PNG":
                    return "image/png";

                default:
                    return "unknown";
            }
        }

        private static StoredFiles MakeEntry(string ext, int FileSize, string OldName, string NewName, long CreatorId)
        {
            return new StoredFiles()
            {
                MimeType = GetMIME(ext),
                ContentSize = FileSize,
                OriginalFilename = OldName,
                ServerFilename = NewName,
                DateCreated = DateTime.UtcNow,
                CreatedBy = CreatorId
            };
        }
    }
}

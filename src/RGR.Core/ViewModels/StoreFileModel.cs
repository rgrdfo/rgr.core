using System.ComponentModel.DataAnnotations;

namespace RGR.Core.ViewModels
{
    public class StoreFileModel
    {
        [Required(ErrorMessage = "Не указано имя файла!")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "Файл не найден!")]
        public byte[] Content { get; set; }
    }
}

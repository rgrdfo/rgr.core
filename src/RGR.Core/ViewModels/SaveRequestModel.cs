using System.ComponentModel.DataAnnotations;

namespace RGR.Core.ViewModels
{
    public class SaveRequestModel
    {
        [Required(ErrorMessage = "Введите название запроса!")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "Строка запроса не может быть пустой!")]
        public string Query { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RGR.Core.ViewModels
{
    public class AddObjectModel
    {
        [Required(ErrorMessage = "Пожалуйста, укажите цену в рублях")]
        private double price;
        public double Price
        { get
            {
                return price;
            }

            set
            {
                if (value > 99999)
                    price = value;
            }
        }

        [Required(ErrorMessage = "Город не указан!")]
        public long CityId { get; set; }

        [Required(ErrorMessage = "Район не указан!")]
        public long DistrictId { get; set; }

        [Required(ErrorMessage = "Улица не указана!")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Номер дома не указан!")]
        public string House { get; set; }

        [Required(ErrorMessage = "Номер квартиры не указан!")]
        public string Flat { get; set; }

        public long DistrictResidentalAreaId { get; set; }

    }
}

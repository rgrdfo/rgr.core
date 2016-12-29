using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.API.DTO
{
    public class SearchRequestDTO
    {
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public bool? Room1 { get; set; }
        public bool? Room2 { get; set; }
        public bool? Room3 { get; set; }
        public bool? Room4 { get; set; }
        public bool? Room5 { get; set; }
        public bool? Room6 { get; set; }
        /// <summary>
        /// Планировка комнат: Раздельные = 12, Смежные = 13, Смежно-раздельные = 14, Икарус = 15, Свободная планировка = 16
        /// </summary>
        public byte? RoomPlanning { get; set; }
        public bool LandPurposeDach { get; set; }
        public bool LandPurposePers { get; set; }
        public bool LandPurposeLPH { get; set; }
        public bool BuildingPurposeShop { get; set; }
        public bool BuildingPurposeOffice { get; set; }
        public bool BuildingPurposeProduct { get; set; }
        public bool BuildingPurposeStorage { get; set; }
        public bool BuildingPurposeSalePt { get; set; }
        public bool BuildingPurposeCafe { get; set; }
        public bool BuildingPurposeService { get; set; }
        public bool BuildingPurposeHotel { get; set; }
        public bool BuildingPurposeFree { get; set; }
    }
}

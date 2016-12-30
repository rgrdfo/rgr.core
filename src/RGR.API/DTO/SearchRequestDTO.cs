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
        public bool? LandPurposeDach { get; set; }
        public bool? LandPurposePers { get; set; }
        public bool? LandPurposeLPH { get; set; }
        public bool? BuildingPurposeShop { get; set; }
        public bool? BuildingPurposeOffice { get; set; }
        public bool? BuildingPurposeProduct { get; set; }
        public bool? BuildingPurposeStorage { get; set; }
        public bool? BuildingPurposeSalePt { get; set; }
        public bool? BuildingPurposeCafe { get; set; }
        public bool? BuildingPurposeService { get; set; }
        public bool? BuildingPurposeHotel { get; set; }
        public bool? BuildingPurposeFree { get; set; }
        public bool? WaterHotCenter { get; set; }
        public bool? WaterHotAuton { get; set; }
        public bool? WaterColdCenter { get; set; }
        public bool? WaterColdWell { get; set; }
        public bool? WaterSummer { get; set; }
        public bool? WaterNone { get; set; }
        public bool? ElectricySupplied { get; set; }
        public bool? ElectricyConnected{ get; set; }
        public bool? ElectricyPossible { get; set; }
        public bool? HeatCentral { get; set; }
        public bool? HeatFuel{ get; set; }
        public bool? HeatGas{ get; set; }
        public bool? HeatElectro{ get; set; }
        public bool? HeatNone{ get; set; }
        /// <summary>
        /// Канализация: 207 - Автоматическая, 313 - Центральная, 314 - Шамбо, 312 - Нет
        /// </summary>
        public byte? Sewer { get; set; }
        public double? AreaFrom { get; set; }
        public double? AreaTo { get; set; }
        public double? AreaLivingFrom{ get; set; }
        public double? AreaLivingTo{ get; set; }
        public double? AreaKitchenFrom{ get; set; }
        public double? AreaKitchenTo{ get; set; }
        public int? MinFloor { get; set; }
        public int? MaxFloor{ get; set; }
        public bool? NoFirstFloor { get; set; }
        public bool? NoLastFloor { get; set; }
        public int? MinHouseFloors { get; set; }
        public int? MaxHouseFloors { get; set; }
        public bool? WCSeparated { get; set; }
        public int? BalconiesCount { get; set; }
        public int? LogiasCount { get; set; }
        /// <summary>
        /// Тип дома: 138 - Барак, 143 - Общежитие, 144 - Сталинка, 146 - Хрущ, 145 - Брежневка, 137 - Улучшеной планировки, 142 - Новой планировки, 139 - Свободной планировки
        /// </summary>
        public byte? HouseType { get; set; }
        public bool? HouseMaterialWood { get; set; }
        public bool? HouseMaterialBrixk { get; set; }
        public bool? HouseMaterialPanel { get; set; }
        public bool? HouseMaterialMonolite { get; set; }
        public bool? HouseMaterialOther { get; set; }
        /// <summary>
        /// Состояние объекта: 85 - после строителей, 86 - треб. кап. ремонт, 87 - треб. частичный ремонт, 88 - треб. косм. ремонт, 89 - удовл., 90 - хор., 91 - отл., 92 - евроремонт
        /// </summary>
        public byte? OobjectState { get; set; }
        public long CityId { get; set; } = 4;
        public long? DistrictId { get; set; }
        public long AreaId { get; set; }
        public string Streets { get; set; }
        public string Agencies { get; set; }
        public string Agents { get; set; }
        public DateTime? StartDate { get; set; }
        public bool WithPhotoOnly { get; set; } = false;
    }
}

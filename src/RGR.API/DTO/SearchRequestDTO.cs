using RGR.API.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RGR.API.DTO
{
    public class SearchRequestDTO
    {
        public short EstateType { get; set; } = (short) EstateTypes.Flat;
        public bool IsCottage { get; set; } = false;
        public bool PricePerMetter { get; set; } = false; //Определяет, является ли указанная цена ценой за квадратный метр, или за всю площадь
        public double? PriceFrom { get; set; } = null;
        public double? PriceTo { get; set; } = null;
        public bool? Room1 { get; set; } = null;
        public bool? Room2 { get; set; } = null;
        public bool? Room3 { get; set; } = null;
        public bool? Room4 { get; set; } = null;
        public bool? Room5 { get; set; } = null;
        public bool? Room6 { get; set; } = null;
        /// <summary>
        /// Планировка комнат: Раздельные = 12, Смежные = 13, Смежно-раздельные = 14, Икарус = 15, Свободная планировка = 16
        /// </summary>
        public byte? RoomPlanning { get; set; } = null;
        public bool? LandPurposeDach { get; set; } = null;
        public bool? LandPurposePers { get; set; } = null;
        public bool? LandPurposeLPH { get; set; } = null;
        public bool? BuildingPurposeShop { get; set; } = null;
        public bool? BuildingPurposeOffice { get; set; } = null;
        public bool? BuildingPurposeProduct { get; set; } = null;
        public bool? BuildingPurposeStorage { get; set; } = null;
        public bool? BuildingPurposeSalePt { get; set; } = null;
        public bool? BuildingPurposeCafe { get; set; } = null;
        public bool? BuildingPurposeService { get; set; } = null;
        public bool? BuildingPurposeHotel { get; set; } = null;
        public bool? BuildingPurposeFree { get; set; } = null;
        public bool? WaterHotCenter { get; set; } = null;
        public bool? WaterHotAuton { get; set; } = null;
        public bool? WaterColdCenter { get; set; } = null;
        public bool? WaterColdWell { get; set; } = null;
        public bool? WaterSummer { get; set; } = null;
        public bool? WaterNone { get; set; } = null;
        public bool? ElectricySupplied { get; set; } = null;
        public bool? ElectricyConnected{ get; set; } = null;
        public bool? ElectricyPossible { get; set; } = null;
        public bool? HeatCentral { get; set; } = null;
        public bool? HeatFuel{ get; set; } = null;
        public bool? HeatGas{ get; set; } = null;
        public bool? HeatElectro{ get; set; } = null;
        public bool? HeatNone{ get; set; } = null;
        /// <summary>
        /// Канализация: 207 - Автоматическая, 313 - Центральная, 314 - Шамбо, 312 - Нет
        /// </summary>
        public byte? Sewer { get; set; } = null;
        public double? AreaFrom { get; set; } = null;
        public double? AreaTo { get; set; } = null;
        public double? AreaLivingFrom{ get; set; } = null;
        public double? AreaLivingTo{ get; set; } = null;
        public double? AreaKitchenFrom{ get; set; } = null;
        public double? AreaKitchenTo{ get; set; } = null;
        public int? MinFloor { get; set; } = null;
        public int? MaxFloor{ get; set; } = null;
        public bool? NoFirstFloor { get; set; } = null;
        public bool? NoLastFloor { get; set; } = null;
        public int? MinHouseFloors { get; set; } = null;
        public int? MaxHouseFloors { get; set; } = null;
        public bool? WCSeparated { get; set; } = null;
        public int? BalconiesCount { get; set; } = null;
        public int? LoggiasCount { get; set; } = null;
        /// <summary>
        /// Тип дома: 138 - Барак, 143 - Общежитие, 144 - Сталинка, 146 - Хрущ, 145 - Брежневка, 137 - Улучшеной планировки, 142 - Новой планировки, 139 - Свободной планировки
        /// </summary>
        public byte? HouseType { get; set; } = null;
        public bool? HouseMaterialWood     { get; set; } = null;
        public bool? HouseMaterialBrick    { get; set; } = null;
        public bool? HouseMaterialPanel    { get; set; } = null;
        public bool? HouseMaterialMonolite { get; set; } = null;
        public bool? HouseMaterialOther    { get; set; } = null;
        /// <summary>
        /// Состояние объекта: 85 - после строителей, 86 - треб. кап. ремонт, 87 - треб. частичный ремонт, 88 - треб. косм. ремонт, 89 - удовл., 90 - хор., 91 - отл., 92 - евроремонт
        /// </summary>
        public byte? OobjectState { get; set; } = null;
        public long CityId { get; set; } = 4;
        public long? DistrictId { get; set; } = null;
        public long? AreaId { get; set; } = null;
        public string Streets { get; set; } = null;
        public string Agencies { get; set; } = null;
        public string Agents { get; set; } = null;
        public DateTime? StartDate { get; set; } = null;
        public bool WithPhotoOnly { get; set; } = false;
    }
}

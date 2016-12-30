using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectMainProperties
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public double? RentPerDay { get; set; }
        public double? RentPerMonth { get; set; }
        public string Security { get; set; }
        public long? Currency { get; set; }
        public long? PropertyType { get; set; }
        public bool? Negotiable { get; set; }
        public bool? ResidencePermit { get; set; }
        public double? CelingHeight { get; set; }
        public double? AtticHeight { get; set; }
        public long? Yard { get; set; }
        public long? OwnerShare { get; set; }
        public string AddCommercialBuildings { get; set; }
        public double? ActualUsableFloorArea { get; set; }
        public double? FirstFloorDownSet { get; set; }
        public string Title { get; set; }
        public bool? MortgagePossibility { get; set; }
        public long? MortgageBank { get; set; }
        public string ObjectUsage { get; set; }
        public bool? NonResidenceUsage { get; set; }
        public long? BuildingClass { get; set; }
        public int? WindowsCount { get; set; }
        public int? PrescriptionsCount { get; set; }
        public int? FamiliesCount { get; set; }
        public int? OwnersCount { get; set; }
        public int? PhoneLinesCount { get; set; }
        public int? LevelsCount { get; set; }
        public int? FacadeWindowsCount { get; set; }
        public bool? UtilitiesRentExspensive { get; set; }
        public string ShortDescription { get; set; }
        public string FloorMaterial { get; set; }
        public string BuildingMaterial { get; set; }
        public string LandAssignment { get; set; }
        public string ObjectAssignment { get; set; }
        public bool? HasWeights { get; set; }
        public bool? HasPhotos { get; set; }
        public bool? NewBuilding { get; set; }
        public double? TotalArea { get; set; }
        public long? Landmark { get; set; }
        public string ReleaseInfo { get; set; }
        public bool? HasParking { get; set; }
        public long? BuildingPeriod { get; set; }
        public double? BigRoomFloorArea { get; set; }
        public double? BuildingFloor { get; set; }
        public double? KitchenFloorArea { get; set; }
        public double? LandArea { get; set; }
        public double? LandFloorFactical { get; set; }
        public long? Loading { get; set; }
        public string FullDescription { get; set; }
        public long? EntranceToObject { get; set; }
        public bool? AbilityForMachineryEntrance { get; set; }
        public string Documents { get; set; }
        public bool? Prepayment { get; set; }
        public string Notes { get; set; }
        public long? RemovalReason { get; set; }
        public string LivingPeoples { get; set; }
        public string LivingPeolplesDescription { get; set; }
        public long? EntryLocation { get; set; }
        public double? DistanceToCity { get; set; }
        public double? DistanceToSea { get; set; }
        public string FootageExplanation { get; set; }
        public string Advertising1 { get; set; }
        public string Advertising2 { get; set; }
        public string Advertising3 { get; set; }
        public string Advertising4 { get; set; }
        public string Advertising5 { get; set; }
        public long? Relief { get; set; }
        public bool? SpecialOffer { get; set; }
        public string SpecialOfferDescription { get; set; }
        public DateTime? LeaseTime { get; set; }
        public bool? Urgently { get; set; }
        public double? BuildingReadyPercent { get; set; }
        public long? HouseType { get; set; }
        public long? FlatType { get; set; }
        public long? BuildingType { get; set; }
        public bool? Exchange { get; set; }
        public string ExchangeConditions { get; set; }
        public bool? HousingStock { get; set; }
        public long? Foundation { get; set; }
        public double? Price { get; set; }
        public double? PricePerUnit { get; set; }
        public double? PricePerHundred { get; set; }
        public double? OwnerPrice { get; set; }
        public int? PricingZone { get; set; }
        public long? HowToReach { get; set; }
        public double? ElectricPower { get; set; }
        public int? FloorNumber { get; set; }
        public int? TotalFloors { get; set; }
        public long? ContractorCompany { get; set; }
        public string RentOverpayment { get; set; }
        public double? RealPrice { get; set; }
        public string MetricDescription { get; set; }
        public string SellConditions { get; set; }
        public bool? Exclusive { get; set; }
        public double? MultilistingBonus { get; set; }
        public long? MultilistingBonusType { get; set; }
        public long? ContactPersonId { get; set; }
        public bool? IsSetNumberAgency { get; set; }
        public short? ContactPhone { get; set; }
        public long? ContactCompanyId { get; set; }

        public virtual Companies ContactCompany { get; set; }
        public virtual Users ContactPerson { get; set; }
        public virtual EstateObjects Object { get; set; }
    }
}

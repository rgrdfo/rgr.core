using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectAdditionalProperties
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public string ViewFromWindows { get; set; }
        public int? BuildingYear { get; set; }
        public string AdditionalBuildings { get; set; }
        public bool? ExtensionsLegality { get; set; }
        public long? Builder { get; set; }
        public int? BalconiesCount { get; set; }
        public int? RoomsCount { get; set; }
        public int? LoggiasCount { get; set; }
        public int? BedroomsCount { get; set; }
        public int? BaywindowsCount { get; set; }
        public long? Roof { get; set; }
        public string ObjectName { get; set; }
        public long? Fencing { get; set; }
        public long? RoomPlanning { get; set; }
        public string Basement { get; set; }
        public bool? CorrectPlanning { get; set; }
        public long? FlatLocation { get; set; }
        public string WindowsLocation { get; set; }
        public string WindowsMaterial { get; set; }
        public bool? Redesign { get; set; }
        public bool? RedesignLegality { get; set; }
        public long? PlotForm { get; set; }
        public int? FlatRoomsCount { get; set; }
        public int? ErkersCount { get; set; }
        public bool? RegistrationPosibility { get; set; }
        public long? OwnerPart { get; set; }
        public long? Burdens { get; set; }
        public DateTime? RentDate { get; set; }
        public long? Court { get; set; }
        public long? Fence { get; set; }
        public string Loading { get; set; }
        public long? Environment { get; set; }
        public bool? RentWithServices { get; set; }
        public bool? Auction { get; set; }
        public long? Placement { get; set; }
        public long? AgreementType { get; set; }
        public string AgreementNumber { get; set; }
        public DateTime? AgreementStartDate { get; set; }
        public DateTime? AgreementEndDate { get; set; }
        public string Comission { get; set; }
        public bool? AgencyPayment { get; set; }
        public string PaymentCondition { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

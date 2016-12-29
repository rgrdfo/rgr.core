using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoObjectInfos
    {
        public long Id { get; set; }
        public long GeoObjectId { get; set; }
        public string Number { get; set; }
        public string Liter { get; set; }
        public int? EntranceCount { get; set; }
        public bool Community { get; set; }
        public string CommunityName { get; set; }
        public string BuildingMaterial { get; set; }
        public int? FloorsCount { get; set; }
        public string Planning { get; set; }
        public int? BuildYear { get; set; }
        public string Description { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Photo { get; set; }
        public string Builder { get; set; }
        public string CelingMaterial { get; set; }
        public bool? Gas { get; set; }
        public bool Locked { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual GeoObjects GeoObject { get; set; }
    }
}

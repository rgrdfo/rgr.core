using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoLandmarks
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public long DistrictId { get; set; }
        public long AreaId { get; set; }
        public long StreetId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual GeoResidentialAreas Area { get; set; }
        public virtual GeoCities City { get; set; }
        public virtual GeoDistricts District { get; set; }
        public virtual GeoStreets Street { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Addresses
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public long CountryId { get; set; }
        public long RegionId { get; set; }
        public long CityId { get; set; }
        public long RegionDistrictId { get; set; }
        public long? CityDistrictId { get; set; }
        public long? DistrictResidentialAreaId { get; set; }
        public long? StreetId { get; set; }
        public string House { get; set; }
        public string Block { get; set; }
        public string Flat { get; set; }
        public string Land { get; set; }
        public double? Latitude { get; set; }
        public double? Logitude { get; set; }

        public virtual GeoDistricts CityDistrict { get; set; }
        public virtual GeoCities City { get; set; }
        public virtual GeoCountries Country { get; set; }
        public virtual GeoResidentialAreas DistrictResidentialArea { get; set; }
        public virtual EstateObjects Object { get; set; }
        public virtual GeoRegionDistricts RegionDistrict { get; set; }
        public virtual GeoRegions Region { get; set; }
        public virtual GeoStreets Street { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoCities
    {
        public GeoCities()
        {
            Addresses = new HashSet<Addresses>();
            Companies = new HashSet<Companies>();
            GeoDistricts = new HashSet<GeoDistricts>();
            GeoLandmarks = new HashSet<GeoLandmarks>();
        }

        public long Id { get; set; }
        public long RegionDistrictId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<Companies> Companies { get; set; }
        public virtual ICollection<GeoDistricts> GeoDistricts { get; set; }
        public virtual ICollection<GeoLandmarks> GeoLandmarks { get; set; }
        public virtual GeoRegionDistricts RegionDistrict { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoRegionDistricts
    {
        public GeoRegionDistricts()
        {
            Addresses = new HashSet<Addresses>();
            GeoCities = new HashSet<GeoCities>();
        }

        public long Id { get; set; }
        public long RegionId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoCities> GeoCities { get; set; }
        public virtual GeoRegions Region { get; set; }
    }
}

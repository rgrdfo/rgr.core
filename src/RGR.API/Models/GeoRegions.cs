using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoRegions
    {
        public GeoRegions()
        {
            Addresses = new HashSet<Addresses>();
            GeoRegionDistricts = new HashSet<GeoRegionDistricts>();
        }

        public long Id { get; set; }
        public long CountryId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoRegionDistricts> GeoRegionDistricts { get; set; }
        public virtual GeoCountries Country { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoCountries
    {
        public GeoCountries()
        {
            Addresses = new HashSet<Addresses>();
            GeoRegions = new HashSet<GeoRegions>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoRegions> GeoRegions { get; set; }
    }
}

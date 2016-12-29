using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoResidentialAreas
    {
        public GeoResidentialAreas()
        {
            Addresses = new HashSet<Addresses>();
            GeoLandmarks = new HashSet<GeoLandmarks>();
            GeoStreets = new HashSet<GeoStreets>();
        }

        public long Id { get; set; }
        public long DistrictId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Bounds { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoLandmarks> GeoLandmarks { get; set; }
        public virtual ICollection<GeoStreets> GeoStreets { get; set; }
        public virtual GeoDistricts District { get; set; }
    }
}

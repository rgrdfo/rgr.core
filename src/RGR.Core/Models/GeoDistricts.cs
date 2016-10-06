using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoDistricts
    {
        public GeoDistricts()
        {
            Addresses = new HashSet<Addresses>();
            GeoLandmarks = new HashSet<GeoLandmarks>();
            GeoResidentialAreas = new HashSet<GeoResidentialAreas>();
        }

        public long Id { get; set; }
        public long CityId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Bounds { get; set; }
        public string Description { get; set; }
        public int Population { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoLandmarks> GeoLandmarks { get; set; }
        public virtual ICollection<GeoResidentialAreas> GeoResidentialAreas { get; set; }
        public virtual GeoCities City { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoStreets
    {
        public GeoStreets()
        {
            Addresses = new HashSet<Addresses>();
            GeoLandmarks = new HashSet<GeoLandmarks>();
            GeoObjects = new HashSet<GeoObjects>();
        }

        public long Id { get; set; }
        public long AreaId { get; set; }
        public string Name { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<GeoLandmarks> GeoLandmarks { get; set; }
        public virtual ICollection<GeoObjects> GeoObjects { get; set; }
        public virtual GeoResidentialAreas Area { get; set; }
    }
}

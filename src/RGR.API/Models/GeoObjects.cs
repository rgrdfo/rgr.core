using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class GeoObjects
    {
        public GeoObjects()
        {
            GeoObjectInfos = new HashSet<GeoObjectInfos>();
        }

        public long Id { get; set; }
        public long StreetId { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<GeoObjectInfos> GeoObjectInfos { get; set; }
        public virtual GeoStreets Street { get; set; }
    }
}

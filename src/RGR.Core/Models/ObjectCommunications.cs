using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectCommunications
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public string Water { get; set; }
        public string Gas { get; set; }
        public long? Sewer { get; set; }
        public string Heating { get; set; }
        public string Phone { get; set; }
        public string Tubes { get; set; }
        public string Electricy { get; set; }
        public string SanFurniture { get; set; }
        public bool? HasGasMeter { get; set; }
        public bool? HasColdWaterMeter { get; set; }
        public bool? HasHotWaterMeter { get; set; }
        public bool? HasElectricyMeter { get; set; }
        public bool? HasInternet { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

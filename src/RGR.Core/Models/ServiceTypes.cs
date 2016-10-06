using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ServiceTypes
    {
        public ServiceTypes()
        {
            ServiceLogItems = new HashSet<ServiceLogItems>();
        }

        public long Id { get; set; }
        public string ServiceName { get; set; }
        public long ProvidedId { get; set; }
        public decimal Tax { get; set; }
        public long? Measure { get; set; }
        public string Description { get; set; }
        public long? Subject { get; set; }
        public string Geo { get; set; }
        public short ServiceStatus { get; set; }
        public decimal Rdvshare { get; set; }
        public string Rules { get; set; }
        public string Examples { get; set; }
        public string ContractNumber { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ContractScan { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<ServiceLogItems> ServiceLogItems { get; set; }
        public virtual Companies Provided { get; set; }
    }
}

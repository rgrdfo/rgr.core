using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ServiceLogItems
    {
        public long Id { get; set; }
        public long ServiceId { get; set; }
        public decimal Volume { get; set; }
        public decimal Summary { get; set; }
        public decimal Rdvsummary { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        public virtual Companies Company { get; set; }
        public virtual ServiceTypes Service { get; set; }
        public virtual Users User { get; set; }
    }
}

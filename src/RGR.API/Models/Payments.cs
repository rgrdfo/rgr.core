using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Payments
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CompanyId { get; set; }
        public short Direction { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public bool Payed { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DatePayed { get; set; }

        public virtual Companies Company { get; set; }
        public virtual Users User { get; set; }
    }
}

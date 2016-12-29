using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectPriceChangements
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public long? Currency { get; set; }
        public double? Value { get; set; }
        public DateTime? DateChanged { get; set; }
        public long? ChangedBy { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

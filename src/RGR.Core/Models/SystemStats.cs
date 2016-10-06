using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class SystemStats
    {
        public long Id { get; set; }
        public short StatType { get; set; }
        public DateTime StatDateTime { get; set; }
        public decimal Value { get; set; }
    }
}

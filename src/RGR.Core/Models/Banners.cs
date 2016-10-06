using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Banners
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public short Location { get; set; }
        public short Type { get; set; }
        public string ObjectUrl { get; set; }
        public string LinkUrl { get; set; }
        public int Views { get; set; }
        public int Clicks { get; set; }
        public int ShowProbability { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}

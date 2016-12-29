using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Partners
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string InactiveImageUrl { get; set; }
        public string ActiveImageUrl { get; set; }
        public int Position { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}

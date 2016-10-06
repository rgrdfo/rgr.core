using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Achievments
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime ReachDate { get; set; }
        public string Title { get; set; }
        public string Organizer { get; set; }
        public string ScanUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Users User { get; set; }
    }
}

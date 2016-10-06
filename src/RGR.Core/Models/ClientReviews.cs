using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ClientReviews
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ObjectId { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Description { get; set; }
        public string ScanUrl { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public short Operation { get; set; }

        public virtual Users User { get; set; }
    }
}

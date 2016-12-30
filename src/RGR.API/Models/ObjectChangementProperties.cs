using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectChangementProperties
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public double? PriceChanging { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? ViewDate { get; set; }
        public DateTime? DateMoved { get; set; }
        public DateTime? DateRegisted { get; set; }
        public DateTime? DealDate { get; set; }
        public DateTime? DelayToDate { get; set; }
        public DateTime? PriceChanged { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public long CreatedBy { get; set; }
        public long ChangedBy { get; set; }
        public long StatusChangedBy { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

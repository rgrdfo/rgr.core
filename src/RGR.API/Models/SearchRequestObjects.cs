using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class SearchRequestObjects
    {
        public SearchRequestObjects()
        {
            SearchRequestObjectComments = new HashSet<SearchRequestObjectComments>();
        }

        public long Id { get; set; }
        public long SearchRequestId { get; set; }
        public long EstateObjectId { get; set; }
        public short Status { get; set; }
        public bool New { get; set; }
        public string DeclineReason { get; set; }
        public bool DeclineReasonPrice { get; set; }
        public double? OldPrice { get; set; }
        public short TriggerEvent { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateMoved { get; set; }

        public virtual ICollection<SearchRequestObjectComments> SearchRequestObjectComments { get; set; }
        public virtual EstateObjects EstateObject { get; set; }
        public virtual SearchRequests SearchRequest { get; set; }
    }
}

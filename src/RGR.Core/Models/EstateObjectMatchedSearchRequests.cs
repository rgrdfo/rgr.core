using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class EstateObjectMatchedSearchRequests
    {
        public EstateObjectMatchedSearchRequests()
        {
            EstateObjectMatchedSearchRequestComments = new HashSet<EstateObjectMatchedSearchRequestComments>();
        }

        public long Id { get; set; }
        public long ObjectId { get; set; }
        public long RequestId { get; set; }
        public long RequestUserId { get; set; }
        public short Status { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateMoved { get; set; }
        public string RequestTitle { get; set; }
        public DateTime? RequestDateCreated { get; set; }
        public DateTime? RequestDateDeleted { get; set; }

        public virtual ICollection<EstateObjectMatchedSearchRequestComments> EstateObjectMatchedSearchRequestComments { get; set; }
        public virtual EstateObjects Object { get; set; }
        public virtual SearchRequests Request { get; set; }
        public virtual Users RequestUser { get; set; }
    }
}

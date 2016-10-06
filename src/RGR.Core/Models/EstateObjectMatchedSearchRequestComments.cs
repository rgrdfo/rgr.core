using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class EstateObjectMatchedSearchRequestComments
    {
        public long Id { get; set; }
        public long MatchedRequestId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual EstateObjectMatchedSearchRequests MatchedRequest { get; set; }
    }
}

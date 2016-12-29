using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class SearchRequests
    {
        public SearchRequests()
        {
            EstateObjectMatchedSearchRequests = new HashSet<EstateObjectMatchedSearchRequests>();
            SearchRequestObjects = new HashSet<SearchRequestObjects>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public string SearchUrl { get; set; }
        public int TimesUsed { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<EstateObjectMatchedSearchRequests> EstateObjectMatchedSearchRequests { get; set; }
        public virtual ICollection<SearchRequestObjects> SearchRequestObjects { get; set; }
        public virtual Users User { get; set; }
    }
}

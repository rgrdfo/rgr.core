using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Comments
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public short EntityType { get; set; }
        public long UserId { get; set; }
        public string AuthorName { get; set; }
        public string AuthiorEmail { get; set; }
        public string Content { get; set; }
        public string RequestData { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Users User { get; set; }
    }
}

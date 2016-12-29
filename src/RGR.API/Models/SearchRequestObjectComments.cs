using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class SearchRequestObjectComments
    {
        public long Id { get; set; }
        public long RequestObjectId { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual SearchRequestObjects RequestObject { get; set; }
        public virtual Users User { get; set; }
    }
}

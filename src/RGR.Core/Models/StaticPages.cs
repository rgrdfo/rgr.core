using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class StaticPages
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Route { get; set; }
        public int Views { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}

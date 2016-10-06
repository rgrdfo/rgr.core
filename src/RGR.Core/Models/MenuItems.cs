using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class MenuItems
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public int Position { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}

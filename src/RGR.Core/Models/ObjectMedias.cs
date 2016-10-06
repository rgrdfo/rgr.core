using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectMedias
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public short MediaType { get; set; }
        public string Title { get; set; }
        public string PreviewUrl { get; set; }
        public string MediaUrl { get; set; }
        public int Views { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public int Position { get; set; }
        public bool IsMain { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class StoredFiles
    {
        public long Id { get; set; }
        public string MimeType { get; set; }
        public long ContentSize { get; set; }
        public string OriginalFilename { get; set; }
        public string ServerFilename { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}

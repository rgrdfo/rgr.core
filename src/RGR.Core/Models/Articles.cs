using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Articles
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public short ArticleType { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ShortContent { get; set; }
        public string FullContent { get; set; }
        public string PreviewImage { get; set; }
        public string VideoLink { get; set; }
        public int Views { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
    }
}

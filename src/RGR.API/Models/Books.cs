using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Books
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public string Author { get; set; }
    }
}

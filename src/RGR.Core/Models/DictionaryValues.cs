using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class DictionaryValues
    {
        public long Id { get; set; }
        public long DictionaryId { get; set; }
        public string Value { get; set; }
        public string ShortValue { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual Dictionaries Dictionary { get; set; }
    }
}

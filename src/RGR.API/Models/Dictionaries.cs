using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Dictionaries
    {
        public Dictionaries()
        {
            DictionaryValues = new HashSet<DictionaryValues>();
        }

        public long Id { get; set; }
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<DictionaryValues> DictionaryValues { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class NonRdvAgents
    {
        public NonRdvAgents()
        {
            ObjectHistory = new HashSet<ObjectHistory>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual ICollection<ObjectHistory> ObjectHistory { get; set; }
    }
}

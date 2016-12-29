using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Passports
    {
        public Passports()
        {
            Clients = new HashSet<Clients>();
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public string IssuesBy { get; set; }
        public DateTime? IssueDate { get; set; }
        public string RegistrationPlace { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Companies
    {
        public Companies()
        {
            Clients = new HashSet<Clients>();
            ObjectMainProperties = new HashSet<ObjectMainProperties>();
            Payments = new HashSet<Payments>();
            ServiceLogItems = new HashSet<ServiceLogItems>();
            ServiceTypes = new HashSet<ServiceTypes>();
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email { get; set; }
        public string LogoImageUrl { get; set; }
        public string LocationSchemeUrl { get; set; }
        public string Description { get; set; }
        public long CompanyType { get; set; }
        public long DirectorId { get; set; }
        public string Branch { get; set; }
        public string ContactPerson { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public bool Inactive { get; set; }
        public bool IsServiceProvider { get; set; }
        public bool Ndspayer { get; set; }
        public string ShortName { get; set; }

        public virtual ICollection<Clients> Clients { get; set; }
        public virtual ICollection<ObjectMainProperties> ObjectMainProperties { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<ServiceLogItems> ServiceLogItems { get; set; }
        public virtual ICollection<ServiceTypes> ServiceTypes { get; set; }
        public virtual ICollection<Users> Users { get; set; }
        public virtual GeoCities City { get; set; }
        public virtual Users Director { get; set; }
    }
}

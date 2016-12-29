using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Clients
    {
        public Clients()
        {
            EstateObjects = new HashSet<EstateObjects>();
            ObjectClients = new HashSet<ObjectClients>();
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? AgreementDate { get; set; }
        public DateTime? AgreementEndDate { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Blacklisted { get; set; }
        public string Commision { get; set; }
        public string Icq { get; set; }
        public string AgreementNumber { get; set; }
        public bool AgencyPayment { get; set; }
        public string Notes { get; set; }
        public string Phone { get; set; }
        public short AgreementType { get; set; }
        public short ClientType { get; set; }
        public string PaymentConditions { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public long PassportId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<EstateObjects> EstateObjects { get; set; }
        public virtual ICollection<ObjectClients> ObjectClients { get; set; }
        public virtual Companies Company { get; set; }
        public virtual Passports Passport { get; set; }
    }
}

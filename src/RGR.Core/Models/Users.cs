using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Users
    {
        public Users()
        {
            Achievments = new HashSet<Achievments>();
            AuditEvents = new HashSet<AuditEvents>();
            ClientReviews = new HashSet<ClientReviews>();
            Comments = new HashSet<Comments>();
            Companies = new HashSet<Companies>();
            EstateObjectMatchedSearchRequests = new HashSet<EstateObjectMatchedSearchRequests>();
            EstateObjects = new HashSet<EstateObjects>();
            ObjectHistory = new HashSet<ObjectHistory>();
            ObjectMainProperties = new HashSet<ObjectMainProperties>();
            Payments = new HashSet<Payments>();
            SearchRequestObjectComments = new HashSet<SearchRequestObjectComments>();
            SearchRequests = new HashSet<SearchRequests>();
            ServiceLogItems = new HashSet<ServiceLogItems>();
            TrainingPrograms = new HashSet<TrainingPrograms>();
        }

        public long Id { get; set; }
        public long CompanyId { get; set; }
        public long RoleId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Icq { get; set; }
        public string Appointment { get; set; }
        public string PhotoUrl { get; set; }
        public bool Blocked { get; set; }
        public bool Activated { get; set; }
        public string CertificateNumber { get; set; }
        public DateTime? CertificationDate { get; set; }
        public DateTime? SeniorityStartDate { get; set; }
        public DateTime? Birthdate { get; set; }
        public long PassportId { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }
        public int Status { get; set; }
        public string AdditionalInformation { get; set; }
        public string PublicLoading { get; set; }
        public DateTime? CertificateEndDate { get; set; }
        public short Notifications { get; set; }

        public virtual ICollection<Achievments> Achievments { get; set; }
        public virtual ICollection<AuditEvents> AuditEvents { get; set; }
        public virtual ICollection<ClientReviews> ClientReviews { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<Companies> Companies { get; set; }
        public virtual ICollection<EstateObjectMatchedSearchRequests> EstateObjectMatchedSearchRequests { get; set; }
        public virtual ICollection<EstateObjects> EstateObjects { get; set; }
        public virtual ICollection<ObjectHistory> ObjectHistory { get; set; }
        public virtual ICollection<ObjectMainProperties> ObjectMainProperties { get; set; }
        public virtual ICollection<Payments> Payments { get; set; }
        public virtual ICollection<SearchRequestObjectComments> SearchRequestObjectComments { get; set; }
        public virtual ICollection<SearchRequests> SearchRequests { get; set; }
        public virtual ICollection<ServiceLogItems> ServiceLogItems { get; set; }
        public virtual ICollection<TrainingPrograms> TrainingPrograms { get; set; }
        public virtual Companies Company { get; set; }
        public virtual Passports Passport { get; set; }
        public virtual Roles Role { get; set; }
    }
}

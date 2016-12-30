using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class EstateObjects
    {
        public EstateObjects()
        {
            Addresses = new HashSet<Addresses>();
            EstateObjectMatchedSearchRequests = new HashSet<EstateObjectMatchedSearchRequests>();
            ObjectAdditionalProperties = new HashSet<ObjectAdditionalProperties>();
            ObjectChangementProperties = new HashSet<ObjectChangementProperties>();
            ObjectClients = new HashSet<ObjectClients>();
            ObjectCommunications = new HashSet<ObjectCommunications>();
            ObjectHistory = new HashSet<ObjectHistory>();
            ObjectMainProperties = new HashSet<ObjectMainProperties>();
            ObjectManagerNotifications = new HashSet<ObjectManagerNotifications>();
            ObjectMedias = new HashSet<ObjectMedias>();
            ObjectPriceChangements = new HashSet<ObjectPriceChangements>();
            ObjectRatingProperties = new HashSet<ObjectRatingProperties>();
            SearchRequestObjects = new HashSet<SearchRequestObjects>();
        }

        public long Id { get; set; }
        public long UserId { get; set; }
        public long ClientId { get; set; }
        public short ObjectType { get; set; }
        public short Operation { get; set; }
        public short Status { get; set; }
        public bool Filled { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<Addresses> Addresses { get; set; }
        public virtual ICollection<EstateObjectMatchedSearchRequests> EstateObjectMatchedSearchRequests { get; set; }
        public virtual ICollection<ObjectAdditionalProperties> ObjectAdditionalProperties { get; set; }
        public virtual ICollection<ObjectChangementProperties> ObjectChangementProperties { get; set; }
        public virtual ICollection<ObjectClients> ObjectClients { get; set; }
        public virtual ICollection<ObjectCommunications> ObjectCommunications { get; set; }
        public virtual ICollection<ObjectHistory> ObjectHistory { get; set; }
        public virtual ICollection<ObjectMainProperties> ObjectMainProperties { get; set; }
        public virtual ICollection<ObjectManagerNotifications> ObjectManagerNotifications { get; set; }
        public virtual ICollection<ObjectMedias> ObjectMedias { get; set; }
        public virtual ICollection<ObjectPriceChangements> ObjectPriceChangements { get; set; }
        public virtual ICollection<ObjectRatingProperties> ObjectRatingProperties { get; set; }
        public virtual ICollection<SearchRequestObjects> SearchRequestObjects { get; set; }
        public virtual Clients Client { get; set; }
        public virtual Users User { get; set; }
    }
}

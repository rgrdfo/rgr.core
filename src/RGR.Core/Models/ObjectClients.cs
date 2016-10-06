using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectClients
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long ObjectId { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Clients Client { get; set; }
        public virtual EstateObjects Object { get; set; }
    }
}

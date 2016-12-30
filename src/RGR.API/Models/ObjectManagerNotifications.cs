using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class ObjectManagerNotifications
    {
        public long Id { get; set; }
        public long ObjectId { get; set; }
        public short NotificationType { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual EstateObjects Object { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class RolePermissionOptions
    {
        public long Id { get; set; }
        public long RolePermissionId { get; set; }
        public short ObjectOperation { get; set; }
        public short ObjectType { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual RolePermissions RolePermission { get; set; }
    }
}

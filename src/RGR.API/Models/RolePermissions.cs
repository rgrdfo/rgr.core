using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class RolePermissions
    {
        public RolePermissions()
        {
            RolePermissionOptions = new HashSet<RolePermissionOptions>();
        }

        public long Id { get; set; }
        public long RoleId { get; set; }
        public long PermissionId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<RolePermissionOptions> RolePermissionOptions { get; set; }
        public virtual Permissions Permission { get; set; }
        public virtual Roles Role { get; set; }
    }
}

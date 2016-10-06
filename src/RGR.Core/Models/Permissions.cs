using System;
using System.Collections.Generic;

namespace RGR.Core.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            RolePermissions = new HashSet<RolePermissions>();
        }

        public long Id { get; set; }
        public string SystemName { get; set; }
        public string DisplayName { get; set; }
        public string PermissionGroup { get; set; }
        public bool OperationContext { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public long CreatedBy { get; set; }
        public long ModifiedBy { get; set; }

        public virtual ICollection<RolePermissions> RolePermissions { get; set; }
    }
}

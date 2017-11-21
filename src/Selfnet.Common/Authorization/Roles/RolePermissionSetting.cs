using System;
using System.Collections.Generic;
using System.Text;

namespace Selfnet.Common.Authorization.Roles
{
    /// <summary>
    /// Used to store setting for a permission for a role.
    /// </summary>
    public class RolePermissionSetting : PermissionSetting
    {
        /// <summary>
        /// Role id.
        /// </summary>
        public virtual int RoleId { get; set; }
    }
}

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Microsoft.AspNet.Identity;
using Selfnet.Authorization.Users;
using Selfnet.Common.Authorization.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Authorization.Roles
{
    public abstract class NetRole<TUser> : RoleBase, IRole<int>, IFullAudited<TUser> where TUser : NetUser<TUser>
    {
        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }

        protected NetRole()
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpRole{TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="displayName">Display name of the role</param>
        protected NetRole(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {
        }

        /// <summary>
        /// Creates a new <see cref="AbpRole{TUser}"/> object.
        /// </summary>
        /// <param name="tenantId">TenantId or null (if this is not a tenant-level role)</param>
        /// <param name="name">Unique role name</param>
        /// <param name="displayName">Display name of the role</param>
        protected NetRole(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {
        }
    }
}

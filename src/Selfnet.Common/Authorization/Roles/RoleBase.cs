﻿using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Common.Authorization.Roles
{
    [Table("Roles")]
    public class RoleBase : FullAuditedEntity<int>, IMayHaveTenant
    {
        /// <summary>
        /// Maximum length of the <see cref="DisplayName"/> property.
        /// </summary>
        public const int MaxDisplayNameLength = 64;

        /// <summary>
        /// Maximum length of the <see cref="Name"/> property.
        /// </summary>
        public const int MaxNameLength = 32;

        /// <summary>
        /// Tenant's Id, if this role is a tenant-level role. Null, if not.
        /// </summary>
        public virtual int? TenantId { get; set; }

        /// <summary>
        /// Unique name of this role.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Display name of this role.
        /// </summary>
        [Required]
        [StringLength(MaxDisplayNameLength)]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Is this a static role?
        /// Static roles can not be deleted, can not change their name.
        /// They can be used programmatically.
        /// </summary>
        public virtual bool IsStatic { get; set; }

        /// <summary>
        /// Is this role will be assigned to new users as default?
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// List of permissions of the role.
        /// </summary>
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermissionSetting> Permissions { get; set; }

        protected RoleBase()
        {
            Name = Guid.NewGuid().ToString("N");
        }

        protected RoleBase(int? tenantId, string displayName)
            : this()
        {
            TenantId = tenantId;
            DisplayName = displayName;
        }

        protected RoleBase(int? tenantId, string name, string displayName)
            : this(tenantId, displayName)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[Role {Id}, Name={Name}]";
        }
    }
}

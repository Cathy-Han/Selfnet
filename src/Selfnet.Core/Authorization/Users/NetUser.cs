using Abp;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Extensions;
using Microsoft.AspNet.Identity;
using Selfnet.Common.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Authorization.Users
{
    public abstract class NetUser<TUser> : UserBase, IUser<long>, IFullAudited<TUser>
        where TUser : NetUser<TUser>
    {
        public virtual TUser DeleterUser { get; set; }

        public virtual TUser CreatorUser { get; set; }

        public virtual TUser LastModifierUser { get; set; }
    }
}

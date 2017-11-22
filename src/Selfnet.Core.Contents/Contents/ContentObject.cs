using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selfnet.Core.Contents.Contents
{
    /// <summary>
    /// content config.
    /// </summary>
    public class ContentObject : Entity<int>, IHasCreationTime,IMayHaveTenant
    {
        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string DisplayModule { get; set; }
        public string ModuleName { get; set; }

        public DateTime CreationTime { get; set; }

        public ICollection<ContentField> Fields { get; set; }
        public int? TenantId { get; set; }
    }
}

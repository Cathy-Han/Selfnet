using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Core.Contents.Contents
{
    /// <summary>
    /// content records.
    /// </summary>
    public class ContentRecord : Entity<long>, IHasCreationTime, IHasDeletionTime, IHasModificationTime,IMayHaveTenant
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RowId { get; set; }
        public byte[] Timestamp { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? TenantId { get; set; }

        public ContentObject Object { get; set; }
    }
}

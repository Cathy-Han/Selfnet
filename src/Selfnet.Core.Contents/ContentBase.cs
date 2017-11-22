using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Selfnet.Core.Contents.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Core.Contents
{
    public abstract class ContentBase : Entity<long>, IHasModificationTime
    {
        public long RecordId { get; set; }

        [ForeignKey("RecordId")]
        public ContentRecord Record { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}

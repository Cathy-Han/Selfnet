using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selfnet.Core.Contents.Configuration
{
    /// <summary>
    /// contents manage system type.
    /// </summary>
    public class CmsType : Entity<int>, IHasCreationTime
    {
        public const string TEXT_CONTENT = "txt";
        public const string PIC_CONTENT = "picture";
        public const string DOCUMENT_CONTENT = "document";
        public const string VIDEO_CONTENT = "video";
        public const string AUDIO_CONTENT = "audio";
        public const string ZIP_CONTENT = "zip";

        /// <summary>
        /// display name.
        /// </summary>
        public virtual string DisplayName { get; set; }
        /// <summary>
        /// type name.
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// table name.
        /// </summary>
        public virtual string TableName { get; set; }
        /// <summary>
        /// creation time
        /// </summary>
        public DateTime CreationTime { get; set; } = DateTime.Now;
    }
}

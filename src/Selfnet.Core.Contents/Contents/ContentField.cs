using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selfnet.Core.Contents.Contents
{
    public class ContentField : Entity<int>, IHasCreationTime
    {
        public const string IntContent = "IntContent";

        public const string LongContent = "LongContent";

        public const string DoubleContent = "DoubleContent";

        public const string DecimalContent = "DecimalContent";

        public const string ShortContent = "TextContent";

        public const string Content = "HtmlContent";

        public string FieldName { get; set; }
        public string CmsType { get; set; }
        public DateTime CreationTime { get; set; }
    }
}

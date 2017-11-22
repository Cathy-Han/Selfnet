using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Selfnet.Core.Contents.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Core.Contents
{
    /// <summary>
    /// txt content .
    /// </summary>
    public class TxtContent : ContentBase
    {
        public const int FieldNameLength = 64;
        public const int TextContentLength = 256;

        public virtual int? IntContent { get; set; }

        public virtual long? LongContent { get; set; }

        public virtual double? DoubleContent { get; set; }

        public virtual decimal? DecimalContent { get; set; }

        [StringLength(TextContentLength)]
        public virtual string TextContent { get; set; }

        public virtual string HtmlContent { get; set; }

    }
}

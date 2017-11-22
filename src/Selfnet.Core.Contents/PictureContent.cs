using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Selfnet.Core.Contents.Contents;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Selfnet.Core.Contents
{
    public class PictureContent : FileContent
    {
        public const string Suffix = "gif,jpeg,png,jpg,psd";

        public string ThumbPath { get; set; }

        public string WaterPath { get; set; }
    }
}

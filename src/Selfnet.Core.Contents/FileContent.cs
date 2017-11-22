using System;
using System.Collections.Generic;
using System.Text;

namespace Selfnet.Core.Contents
{
    public abstract class FileContent : ContentBase
    {
        public string DisplayName { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

    }
}

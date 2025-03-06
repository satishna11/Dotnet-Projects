using System;
using Entity.Common;

namespace Entity.Model
{
    public class NoticeInfo : BaseDbModel

    {
        public int Id{ get; set; }

        public string NoticeTitle { get; set; }

        public string Detail { get; set; }

        public string BackgroundImage { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }

        public int OrderKey { get; set; }
    }
}


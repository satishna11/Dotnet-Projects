using System;
namespace Entity.ViewModel
{
	public class NoticeInfoVM
	{
        public int Id { get; set; }

        public string NoticeTitle { get; set; }

        public string Detail { get; set; }

        public string BackgroundImage { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public int OrderKey { get; set; }
    }
}


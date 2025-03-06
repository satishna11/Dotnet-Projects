using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public  class DashboardVM
    {
        public List<DashboardSliderVM> sliders { get; set; }
        public List<CourseInfoVM> courses { get; set; }
        public CourseInfoVM course { get; set; }
        public List<NoticeInfoVM>notice { get; set; }
       
      
    }
}

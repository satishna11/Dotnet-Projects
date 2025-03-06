using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class CourseInfoVM
    {
        public int InfoID { get; set; }
        public string Title { get; set; }
        public int CourseGroupID { get; set; }
        public string ShortDescription { get; set; }
        public int TotalPrice { get; set; }
        public string CourseTime { get; set; }
        public int MaxStudent { get; set; }
        public string DailyDuration { get; set; }
        public string DetailDEscription { get; set; }  // Corrected here
        public int TrainerID { get; set; }
        public string Thumbnail { get; set; }
        public string TrainerName { get; set; }
        public string CourseGroup { get; set; }
        public string Photo { get; set; }
    }

}

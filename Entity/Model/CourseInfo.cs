using Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class CourseInfo : BaseDbModel
    {
        [Key]
        public int InfoID { get; set; }
        public string Title { get; set; }
        public int CourseGroupID { get; set; }
        public string ShortDescription { get; set; }
        public int TotalPrice { get; set; }
        public string CourseTime { get; set; }
        public int MaxStudent { get; set; }
        public string DailyDuration { get; set; }
        public string DetailDEscription { get; set; }
        public int TrainerID { get; set; }
        public string Thumbnail { get; set; }

        [ForeignKey("CourseGroupID")]
        public virtual CourseGroup CourseGroup { get; set; }

        [ForeignKey("TrainerID")]
        public virtual Trainer Trainer { get; set; }


    }
}

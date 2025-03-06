using Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class CourseGroup : BaseDbModel
    {
        [Key]
        public int CourseGroupID { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }

    }
}

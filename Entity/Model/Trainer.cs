using Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model
{
    public class Trainer : BaseDbModel
    {
        [Key]
        public int TrainerID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Photo { get; set; }
        public string FacebookID { get; set; }
        public string LinkedInID { get; set; }
        public string ShortDescription { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ViewModel
{
    public class StudentVM
    {
        public int StudentID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Photo { get; set; }
        public string CourseInfo{ get; set; }
        public int InfoID { get; set; }
    }
}

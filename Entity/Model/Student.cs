using Entity.Common;
using Entity.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student : BaseDbModel
{
    [Key]
    public int StudentID { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }

    public string Contact { get; set; }
    public string Photo { get; set; }

    public int InfoID { get; set; }  // This column must exist in the Student table

    [ForeignKey("InfoID")]
    public virtual CourseInfo CourseInfo { get; set; }
}

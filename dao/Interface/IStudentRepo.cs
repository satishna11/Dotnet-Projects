using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interface
{
    public interface IStudentRepo
    {
        ResponseData SaveData(Student model);
        ResponseData DeleteData(int id);
        ResponseData<List<Student>> GetAllData(string fname, string email, string contact);
        ResponseData<StudentVM> GetDataByID(int id);
    }
}

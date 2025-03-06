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
    public interface ICourseInfoRepo
    {
        ResponseData SaveData(CourseInfo model);
        ResponseData DeleteData(int id);
        ResponseData<List<CourseInfo>> GetAllData(string title, int? totalprice);
        ResponseData<CourseInfoVM> GetDataByID(int id);
    }
}

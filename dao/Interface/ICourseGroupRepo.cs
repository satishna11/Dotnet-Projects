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
    public interface ICourseGroupRepo
    {
        ResponseData SaveData(CourseGroup model);
        ResponseData DeleteData(int id);
        ResponseData<List<CourseGroup>> GetAllData(string Title, string SubTitle);
        ResponseData<CourseGroupVM> GetDataByID(int id);
    }
}

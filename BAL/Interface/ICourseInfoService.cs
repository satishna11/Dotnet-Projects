using Entity.Common;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface ICourseInfoService
    {
        ResponseData SaveData(CourseInfoVM model);
        ResponseData DeleteData(int id);
        ResponseData<List<CourseInfoVM>> GetAllData(string title, int? totalprice);
        ResponseData<CourseInfoVM> GetDataByID(int id);
    }
}

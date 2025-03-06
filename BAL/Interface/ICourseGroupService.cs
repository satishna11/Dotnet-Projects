using Entity.Common;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface ICourseGroupService
    {
        ResponseData SaveData(CourseGroupVM model);
        ResponseData DeleteData(int id);
        ResponseData<List<CourseGroupVM>> GetAllData(string Title, string SubTitle);
        ResponseData<CourseGroupVM> GetDataByID(int id);
    }
}

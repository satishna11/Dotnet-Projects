using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IUserGroupService
    {
        ResponseData SaveData(UserGroupVM model);
        ResponseData DeleteData(int id);
        ResponseData<List<UserGroupVM>> GetAllData();
        ResponseData<UserGroupVM> GetDataByID(int id);
    }
}

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
    public interface IUserGroupRepo
    {
        ResponseData SaveData(UserGroup model);
        ResponseData DeleteData(int id);
        ResponseData<List<UserGroup>> GetAllData();
        ResponseData<UserGroupVM> GetDataByID(int id);
    }
}

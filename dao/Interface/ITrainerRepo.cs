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
    public interface ITrainerRepo
    {
        ResponseData SaveData(Trainer model);
        ResponseData DeleteData(int id);
        ResponseData<List<Trainer>> GetAllData(string fname, string designation, string fID);
        ResponseData<TrainerVM> GetDataByID(int id);
    }
}

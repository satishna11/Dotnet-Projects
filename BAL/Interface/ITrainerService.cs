using Entity.Common;
using Entity.ViewModel;
using System.Collections.Generic;

namespace BAL.Interface
{
    public interface ITrainerService
    {
       ResponseData<List<TrainerVM>> GetAllData(string fname, string designation,string fID);
       ResponseData<TrainerVM> GetDataByID(int id);

         ResponseData SaveData(TrainerVM model);

         ResponseData DeleteData(int id);

        ResponseData<TrainerVM> GetDataByID();
    }
}

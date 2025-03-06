using Entity.Common;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interface
{
    public interface IDashboardSliderService
    {
        ResponseData SaveData(DashboardSliderVM model);
        ResponseData DeleteData(int id);
        ResponseData<List<DashboardSliderVM>> GetAllData(string SubTitle, string Title, int? Orderkey);
        ResponseData<DashboardSliderVM> GetDataByID(int id);
    }
}

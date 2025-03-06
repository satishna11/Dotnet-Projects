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
    public interface IDashboardSliderRepo
    {
        ResponseData SaveData(DashboardSlider model);
        ResponseData DeleteData(int id);
        ResponseData<List<DashboardSlider>> GetAllData(string SubTitle, string Title, int? Orderkey);
        ResponseData<DashboardSliderVM> GetDataByID(int id);
    }
}

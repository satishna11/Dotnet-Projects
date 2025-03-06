using System;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;

namespace DAO.Interface
{
	public interface INoticeInfoRepo
	{
        ResponseData SaveData(NoticeInfo model);
        ResponseData DeleteData(int id);
        ResponseData<List<NoticeInfo>> GetAllData(string NoticeDetail,string Title,int? Orderkey);
        ResponseData<NoticeInfoVM> GetDataByID(int id);
    }
}


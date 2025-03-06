using System;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;

namespace BAL.Interface
{
	public interface INoticeInfoService
	{

        ResponseData SaveData(NoticeInfoVM model);
        ResponseData DeleteData(int id);
        ResponseData<List<NoticeInfoVM>> GetAllData(string NoticeDetail, string Title, int? Orderkey);
        ResponseData<NoticeInfoVM> GetDataByID(int id);
    }
}


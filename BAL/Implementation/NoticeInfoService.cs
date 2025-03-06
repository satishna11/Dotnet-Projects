using System;
using BAL.Interface;
using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;

namespace BAL.Implementation
{
    public class NoticeInfoService : INoticeInfoService
    {

        INoticeInfoRepo _repo;
        public NoticeInfoService(INoticeInfoRepo repo)
        {
            _repo = repo;
        }

        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<NoticeInfoVM>> GetAllData(string NoticeDetail, string Title, int? Orderkey)
        {

            var res = _repo.GetAllData(NoticeDetail, Title,  Orderkey);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new NoticeInfoVM
                {
                    Id = s.Id.ToInt32(),
                    Detail = s.Detail.ToText(),
                    NoticeTitle = s.NoticeTitle.ToText(),
                   
                    BackgroundImage = s.BackgroundImage.ToString(),  // Map BackgroundImage
                    ValidFrom = s.ValidFrom.ToString("yyyy-MM-dd"),  // Map ValidFrom
                    ValidTo = s.ValidTo.ToString("yyyy-MM-dd"),      // Map ValidTo
                    OrderKey = s.OrderKey
                }).ToList();

                return new ResponseData<List<NoticeInfoVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<NoticeInfoVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<NoticeInfoVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }

        public ResponseData SaveData(NoticeInfoVM model)
        {

            if (string.IsNullOrEmpty(model.NoticeTitle))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Title"
                };
            }
            else if (string.IsNullOrEmpty(model.Detail))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter DashboardSliderInfo"
                };
            }
            
            else
            {
                var mdl = new NoticeInfo();
                mdl.Id = model.Id.ToInt32();
                mdl.Detail = model.Detail.ToText();
                mdl.NoticeTitle = model.NoticeTitle.ToText();
               
                mdl.BackgroundImage = model.BackgroundImage.ToText();  // Map BackgroundImage
                mdl.ValidFrom = Convert.ToDateTime(model.ValidFrom);  // Map ValidFrom
                mdl.ValidTo = Convert.ToDateTime(model.ValidTo);     // Map ValidTo
                mdl.OrderKey = model.OrderKey;
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }

    }
}
    



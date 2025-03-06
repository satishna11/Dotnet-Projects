using BAL.Interface;
using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class DashboardSliderService : IDashboardSliderService
    {
        IDashboardSliderRepo _repo;
        public DashboardSliderService(IDashboardSliderRepo repo)
        {
            _repo = repo;
        }
        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<DashboardSliderVM>> GetAllData(string SubTitle, string Title, int? Orderkey)
        {
            var res = _repo.GetAllData(SubTitle,Title,Orderkey);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new DashboardSliderVM
                {
                    Id = s.Id.ToInt32(),
                    DashboardSliderInfo = s.DashboardSliderInfo.ToText(),
                    Title = s.Title.ToText(),
                    SubTitle = s.SubTitle.ToText(),
                    BackgroundImage = s.BackgroundImage.ToText(),
                    ValidFrom = s.ValidFrom.ToString(),
                    ValidTo = s.ValidTo.ToString(),
                    OrderKey = s.OrderKey.ToInt32()


                }).ToList();

                return new ResponseData<List<DashboardSliderVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<DashboardSliderVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<DashboardSliderVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }

        public ResponseData SaveData(DashboardSliderVM model)
        {
            if (string.IsNullOrEmpty(model.Title))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Course Title"
                };
            }
            else if (string.IsNullOrEmpty(model.SubTitle))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Sub Title"
                };
            }
            else
            {
                var mdl = new DashboardSlider();
                mdl.Id = model.Id > 0 ? model.Id : 0;
                mdl.DashboardSliderInfo = string.IsNullOrWhiteSpace(model.DashboardSliderInfo) ? string.Empty : model.DashboardSliderInfo.Trim();
                mdl.Title = string.IsNullOrWhiteSpace(model.Title) ? string.Empty : model.Title.Trim();
                mdl.SubTitle = string.IsNullOrWhiteSpace(model.SubTitle) ? string.Empty : model.SubTitle.Trim();
                mdl.BackgroundImage = string.IsNullOrWhiteSpace(model.BackgroundImage) ? string.Empty : model.BackgroundImage.Trim();

                DateTime validFrom, validTo;
                if (!DateTime.TryParse(model.ValidFrom, out validFrom))
                {
                    return new ResponseData { Success = false, Message = "Invalid 'ValidFrom' date." };
                }
                if (!DateTime.TryParse(model.ValidTo, out validTo))
                {
                    return new ResponseData { Success = false, Message = "Invalid 'ValidTo' date." };
                }

                mdl.ValidFrom = validFrom;
                mdl.ValidTo = validTo;
                mdl.OrderKey = model.OrderKey > 0 ? model.OrderKey : 0;
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;

                // Save the data
                return _repo.SaveData(mdl);
            }
        }

    }
}

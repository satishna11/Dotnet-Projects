using BAL.Interface;
using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class CourseInfoService : ICourseInfoService
    {
        ICourseInfoRepo _repo;
        public CourseInfoService(ICourseInfoRepo repo)
        {
            _repo = repo;
        }
        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<CourseInfoVM>> GetAllData(string title, int? totalprice)
        {
            var res = _repo.GetAllData(title,totalprice);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new CourseInfoVM
                {
                    InfoID = s.InfoID.ToInt32(),
                    Title = s.Title.ToText(),
                    CourseGroupID = s.CourseGroupID.ToInt32(),
                    ShortDescription = s.ShortDescription.ToText(),
                    TotalPrice = s.TotalPrice.ToInt32(),
                    CourseTime = s.CourseTime,
                    MaxStudent = s.MaxStudent.ToInt32(),
                    DailyDuration = s.DailyDuration.ToText(),
                    DetailDEscription = s.DetailDEscription.ToText(),
                    TrainerID = s.TrainerID.ToInt32(),
                    Thumbnail = s.Thumbnail.ToText(),
                    TrainerName = s.Trainer.FullName.ToText(),
                    Photo=s.Trainer.Photo.ToText(),
                    CourseGroup = s.CourseGroup.Title.ToText(),


                }).ToList();

                return new ResponseData<List<CourseInfoVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<CourseInfoVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<CourseInfoVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }



        public ResponseData SaveData(CourseInfoVM model)
        {
          if (string.IsNullOrEmpty(model.ShortDescription))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Short Description"
                };
            }
            else
            {
                var mdl = new CourseInfo();
                mdl.InfoID = model.InfoID.ToInt32();
                mdl.Title = model.Title.ToText();
                mdl.CourseGroupID = model.CourseGroupID.ToInt32();
                mdl.ShortDescription = model.ShortDescription.ToText();
                mdl.TotalPrice = model.TotalPrice.ToInt32();
                mdl.CourseTime = model.CourseTime.ToText();
                mdl.MaxStudent = model.MaxStudent.ToInt32();
                mdl.DailyDuration = model.DailyDuration.ToText();
                mdl.DetailDEscription = model.DetailDEscription.ToText();
                mdl.TrainerID = model.TrainerID.ToInt32();
                mdl.Thumbnail = model.Thumbnail.ToText();
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }

    }
}

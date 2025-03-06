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
    public class CourseGroupService : ICourseGroupService

    {
        ICourseGroupRepo _repo;
        public CourseGroupService(ICourseGroupRepo repo)
        {

            _repo = repo;
        }
        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<CourseGroupVM>> GetAllData(string Title, string SubTitle)
        {
            var res = _repo.GetAllData(Title, SubTitle);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new CourseGroupVM
                {
                    CourseGroupID = s.CourseGroupID.ToInt32(),
                    Title = s.Title.ToText(),
                    SubTitle = s.SubTitle.ToText(),
                }).ToList();

                return new ResponseData<List<CourseGroupVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<CourseGroupVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<CourseGroupVM> GetDataByID(int id)
        {

            return _repo.GetDataByID(id);

        }

        public ResponseData SaveData(CourseGroupVM model)
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
                    Message = "Enter Subtitle"
                };
            }
            else
            {
                var mdl = new CourseGroup();
                mdl.CourseGroupID = model.CourseGroupID.ToInt32();
                mdl.Title = model.Title.ToText();
                mdl.SubTitle = model.SubTitle.ToText();
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }
    }
}

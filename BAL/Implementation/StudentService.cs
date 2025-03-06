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
    public class StudentService : IStudentService
    {
        IStudentRepo _repo;
        public StudentService(IStudentRepo repo)
        {
            _repo = repo;
        }
        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<StudentVM>> GetAllData(string fname, string email, string contact)
        {
            var res = _repo.GetAllData(fname,email,contact);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new StudentVM
                {
                    StudentID = s.StudentID.ToInt32(),
                    InfoID = s.InfoID.ToInt32(),
                    
                    FullName = s.FullName.ToText(),
                    Email = s.Email.ToText(),
                    Contact = s.Contact.ToText(),
                    Photo = s.Photo.ToText(),
                    CourseInfo = s.CourseInfo.Title.ToText(),


                }).ToList();

                return new ResponseData<List<StudentVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<StudentVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<StudentVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }

        public ResponseData<StudentVM> GetDataByID()
        {
            throw new NotImplementedException();
        }

        public ResponseData SaveData(StudentVM model)
        {
            if (string.IsNullOrEmpty(model.FullName))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Full Name"
                };
            }
            else if (string.IsNullOrEmpty(model.Email))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Designation"
                };
            }
            else
            {
                var mdl = new Student();
                mdl.StudentID = model.StudentID.ToInt32();
                mdl.FullName = model.FullName.ToText();
                mdl.Email = model.Email.ToText();
                mdl.InfoID = model.InfoID.ToInt32();
            
                mdl.Contact = model.Contact.ToText();
                mdl.Photo = model.Photo.ToText();
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }
    }
}

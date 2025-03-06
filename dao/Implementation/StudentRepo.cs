using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAO.Implementation
{
    public class StudentRepo : IStudentRepo
    {
        ApplicationDbContext _context;
        public StudentRepo(ApplicationDbContext context)
        { 
            _context = context;
        }
        public ResponseData DeleteData(int id)
        {
            try
            {
                var dbInfo = _context.Student.Where(x => x.StudentID == id).FirstOrDefault();
                if (dbInfo != null)
                {
                    dbInfo.Status = 0;
                    dbInfo.ModifiedDate = DateTime.Now;
                    _context.SaveChanges();
                    return new ResponseData()
                    {
                        Success = true,
                        Message = "Data deleted Successfully!"
                    };
                }
                else
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Data not found!"
                    };
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResponseData<List<Student>> GetAllData(string fname, string email, string contact)
        {
            try
            {
                var query = _context.Student
                           .Include(s => s.CourseInfo) // Eagerly load the CourseInfo entity
                           .Where(x => x.Status == 1);
                           
                query = query
                    .Where(x => string.IsNullOrEmpty(fname) || x.FullName.Contains(fname))
                    .Where(x => string.IsNullOrEmpty(email) || x.Email.Contains(email))
                    .Where(x => string.IsNullOrEmpty(contact) || x.Contact.Contains(contact));
                    

                var datas = query.ToList();

                return new ResponseData<List<Student>>
                {
                    Success = true,
                    Message = "Data Found!",
                    Data = datas
                };
            }
            catch (Exception ex)
            {
                return new ResponseData<List<Student>>
                {
                    Success = false,
                    Message = $"Error occurred: {ex.Message}"
                };
            }
        }

        public ResponseData<StudentVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .Student
                                    .Where(x => x.StudentID == id).Include(i => i.CourseInfo).AsEnumerable()
                                    .Select(s => new StudentVM
                                    {
                                        StudentID = s.StudentID.ToInt32(),
                                        FullName = s.FullName.ToText(),
                                        Email = s.Email.ToText(),
                                        Contact = s.Contact.ToText(),
                                        Photo = s.Photo.ToText(),
                                        InfoID=s.InfoID.ToInt32(),
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<StudentVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<StudentVM>
                    {
                        Success = true,
                        Message = "Data Found!",
                        Data = datas
                    };
                }
            }
            catch (Exception ex)
            {
                // write to a file
                throw ex;
            }
        }

        public ResponseData SaveData(Student model)
        {
            try
            {
                if (model.StudentID == 0)
                {
                    _context.Student.Add(model);

                    _context.SaveChanges();

                    return new ResponseData
                    {
                        Success = true,
                        Message = "Data Save Success"
                    };
                }
                else
                {
                    var dbInfo = _context.Student.Where(x => x.StudentID == model.StudentID).FirstOrDefault();
                    if (dbInfo != null)
                    {
                        dbInfo.FullName = model.FullName.ToText();
                        dbInfo.Email = model.Email.ToText();
                        dbInfo.Contact = model.Contact.ToText();
                        dbInfo.Photo = model.Photo.ToText();
                        dbInfo.InfoID = model.InfoID.ToInt32();
                        dbInfo.Status = 1;
                        dbInfo.ModifiedDate = DateTime.Now;
                        _context.SaveChanges();
                        return new ResponseData
                        {
                            Success = true,
                            Message = "Data modified Successfully"
                        };
                    }
                    else
                    {
                        return new ResponseData
                        {
                            Success = false,
                            Message = "Data not found!!!"
                        };
                    }

                }
            }
            catch (Exception ex)
            {
                //write ex to a file         // SERILOG
                throw ex;
            }
        }
    }
}

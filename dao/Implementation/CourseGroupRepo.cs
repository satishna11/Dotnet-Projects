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
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;

namespace DAO.Implementation
{
    public class CourseGroupRepo : ICourseGroupRepo
    {
        ApplicationDbContext _context;
        public CourseGroupRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public ResponseData DeleteData(int id)
        {
            try
            {
                var dbInfo = _context.CourseGroup.Where(x => x.CourseGroupID == id).FirstOrDefault();
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

        public ResponseData<List<CourseGroup>> GetAllData(string Title, string SubTitle)
        {
            try
            {
                var datas = _context
                                     .CourseGroup
                                     .Where(x => x.Status == 1
                                     && (string.IsNullOrEmpty(Title) || x.Title.Contains(Title))
                                     && (string.IsNullOrEmpty(SubTitle) || x.SubTitle.Contains(SubTitle))
                                     )
                                     .ToList();
                return new ResponseData<List<CourseGroup>>
                {
                    Success = true,
                    Message = "Data Found!",
                    Data = datas
                };
            }
            catch (Exception ex)
            {
                //write exception to a file
                throw ex;
            }
        }

        public ResponseData<CourseGroupVM> GetDataByID(int id)
        {
            var datas = _context.CourseGroup
                .Where(x => x.CourseGroupID == id)
                .Select(s => new CourseGroupVM
                {
                    CourseGroupID = s.CourseGroupID.ToInt32(),
                    Title = s.Title.ToText(),
                    SubTitle = s.SubTitle.ToText(),

                })
                .FirstOrDefault();
            if (datas == null)
            {
                return new ResponseData<CourseGroupVM>
                {
                    Success = false,
                    Message = "Data Not Found!"

                };
            }
            else
            {
                return new ResponseData<CourseGroupVM>
                {
                    Success = true,
                    Message = "Data Found!",
                    Data = datas
                };
            }
        }

        public ResponseData SaveData(CourseGroup model)
        {
            try
            {
                if (model.CourseGroupID == 0)
                {
                    _context.CourseGroup.Add(model);

                    _context.SaveChanges();

                    return new ResponseData
                    {
                        Success = true,
                        Message = "Data Save Success"

                    };
                }
                else
                {
                    var dbInfo = _context.CourseGroup
                         .Where(x => x.CourseGroupID == model.CourseGroupID)
                         .FirstOrDefault();
                    if (dbInfo != null)
                    {
                        dbInfo.Title = model.Title;
                        dbInfo.SubTitle = model.SubTitle;
                        dbInfo.ModifiedDate = DateTime.Now;
                        _context.SaveChanges();
                        return new ResponseData
                        {
                            Success = true,
                            Message = "Data Modified Successfully!!"

                        };
                    }
                    else
                    {
                        return new ResponseData
                        {
                            Success = false,
                            Message = "Data Not Found!!"

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

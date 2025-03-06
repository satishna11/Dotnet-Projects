using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementation
{
    public class CourseInfoRepo : ICourseInfoRepo
    {
        ApplicationDbContext _context;

        public CourseInfoRepo(ApplicationDbContext context)
        { 
            _context = context;
        }
        public ResponseData DeleteData(int id)
        {
            try
            {
                var dbInfo = _context.CourseInfo.Where(x => x.InfoID == id).FirstOrDefault();
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

        public ResponseData<List<CourseInfo>> GetAllData(string title, int? totalprice)
        {
            try
            {
                var query = _context.CourseInfo.Where(x => x.Status == 1);

                query = query
                    .Where(x => string.IsNullOrEmpty(title) || x.Title.Contains(title))
                    .Where(x => !totalprice.HasValue || x.TotalPrice == totalprice.Value);

                var datas = query
                    .Include(i => i.CourseGroup)
                    .Include(i => i.Trainer)
                    .ToList();

                return new ResponseData<List<CourseInfo>>
                {
                    Success = true,
                    Message = "Data Found!",
                    Data = datas
                };
            }
            catch (Exception ex)
            {
                //write exception to a file or log it as needed
                throw ex;
            }
        }

        public ResponseData<CourseInfoVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .CourseInfo
                                    .Where(x => x.InfoID == id)
                                    .Include(i => i.CourseGroup)
                                    .Include(i => i.Trainer)
                                    .AsEnumerable()
                                    .Select(s => new CourseInfoVM
                                    {
                                        Title = s.Title.ToText(),
                                        CourseGroupID = s.CourseGroupID.ToInt32(),
                                        ShortDescription = s.ShortDescription.ToText(),
                                        TotalPrice = s.TotalPrice.ToInt32(),
                                        CourseTime = s.CourseTime.ToText(),
                                        MaxStudent = s.MaxStudent.ToInt32(),
                                        DailyDuration = s.DailyDuration.ToText(),
                                        DetailDEscription = s.DetailDEscription.ToText(),
                                        TrainerID = s.TrainerID.ToInt32(),
                                        Thumbnail = s.Thumbnail.ToText(),
                                        CourseGroup = s.CourseGroup.Title.ToText(),
                                        TrainerName = s.Trainer.FullName.ToText(),
                                        Photo = s.Trainer.Photo.ToText(),
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<CourseInfoVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<CourseInfoVM>
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

        public ResponseData SaveData(CourseInfo model)
        {
            try
            {
                if (model.InfoID == 0)
                {
                    _context.CourseInfo.Add(model);

                    _context.SaveChanges();

                    return new ResponseData
                    {
                        Success = true,
                        Message = "Data Save Success"
                    };
                }
                else
                {
                    var dbInfo = _context.CourseInfo.Where(x => x.InfoID == model.InfoID).FirstOrDefault();
                    if (dbInfo != null)
                    {
                        dbInfo.Title = model.Title.ToText();
                        dbInfo.CourseGroupID = model.CourseGroupID.ToInt32();
                        dbInfo.ShortDescription = model.ShortDescription.ToText();
                        dbInfo.TotalPrice = model.TotalPrice.ToInt32();
                        dbInfo.CourseTime = model.CourseTime.ToText();
                        dbInfo.MaxStudent = model.MaxStudent.ToInt32();
                        dbInfo.DailyDuration = model.DailyDuration.ToText();
                        dbInfo.DetailDEscription = model.DetailDEscription.ToText();
                        dbInfo.TrainerID = model.TrainerID.ToInt32();
                        dbInfo.Thumbnail = model.Thumbnail.ToText();
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

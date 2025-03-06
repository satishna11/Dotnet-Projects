using System;
using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using Microsoft.EntityFrameworkCore;

namespace DAO.Implementation
{
    public class NoticeInfoRepo : INoticeInfoRepo
    {
        ApplicationDbContext _context;
        public NoticeInfoRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public ResponseData DeleteData(int id)
        {
            try
            {
                var oldData = _context.NoticeInfo.Where(x => x.Id == id).FirstOrDefault();
                if (oldData == null)
                {
                    return new ResponseData()
                    {
                        Success = false,
                        Message = "Old Information Not Found!"
                    };
                }
                else
                {
                    oldData.Status = 0;
                    oldData.ModifiedDate = DateTime.Now;
                    //oldData.ModifiedBy = 

                    _context.SaveChanges();

                    return new ResponseData()
                    {
                        Success = true,
                        Message = "Data deleted Successfully!"
                    };
                }
            }
            catch (Exception ex)
            {
                //write exception to filie
                throw ex;
            }
        }

        public ResponseData<List<NoticeInfo>> GetAllData(string NoticeDetail, string Title, int? Orderkey)
        {

            try
            {
                var query = _context.NoticeInfo.Where(x => x.Status == 1);

                query = query
                    .Where(x => string.IsNullOrEmpty(NoticeDetail) || x.Detail.Contains(NoticeDetail))
                    .Where(x => string.IsNullOrEmpty(Title) || x.NoticeTitle.Contains(Title))
                    
                    .Where(x => !Orderkey.HasValue || x.OrderKey == Orderkey.Value);

                var datas = query.ToList();

                return new ResponseData<List<NoticeInfo>>
                {
                    Success = true,
                    Message = "Data Found!",
                    Data = datas
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ResponseData<NoticeInfoVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .NoticeInfo
                                    .Where(x => x.Id == id)
                                    .Select(s => new NoticeInfoVM
                                    {
                                        Id = s.Id.ToInt32(),
                                        NoticeTitle = s.NoticeTitle.ToText(),
                                        Detail = s.Detail.ToText(),
                                       
                                        BackgroundImage = s.BackgroundImage,  // Map BackgroundImage
                                        ValidFrom = s.ValidFrom.ToString("yyyy-MM-dd"),  // Map ValidFrom
                                        ValidTo = s.ValidTo.ToString("yyyy-MM-dd"),      // Map ValidTo
                                        OrderKey = s.OrderKey
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<NoticeInfoVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<NoticeInfoVM>
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

        public ResponseData SaveData(NoticeInfo model)
        {
            try
            {
                var existingSlider = _context.NoticeInfo.FirstOrDefault(x => x.Id == model.Id);

                if (existingSlider != null)
                {
                    // Updating the existing record
                    existingSlider.NoticeTitle = model.NoticeTitle;
                    existingSlider.Detail = model.Detail;
                   
                    existingSlider.BackgroundImage = model.BackgroundImage;
                    existingSlider.ValidFrom = model.ValidFrom;
                    existingSlider.ValidTo = model.ValidTo;
                    existingSlider.OrderKey = model.OrderKey;
                    existingSlider.Status = model.Status;
                    existingSlider.ModifiedDate = DateTime.Now; // assuming you track modifications

                    _context.NoticeInfo.Update(existingSlider);
                }
                else
                {
                    // Creating a new record
                    model.CreatedDate = DateTime.Now;
                    _context.NoticeInfo.Add(model);
                }

                _context.SaveChanges();

                return new ResponseData
                {
                    Success = true,
                    Message = existingSlider != null ? "Data updated successfully" : "Data saved successfully"
                };
            }
            catch (Exception ex)
            {
                throw ex;  // Logging the exception or using a logger like Serilog is recommended
            }
        }


    }
}
    



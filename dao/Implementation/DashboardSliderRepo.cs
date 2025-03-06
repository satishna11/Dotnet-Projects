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
    public class DashboardSliderRepo : IDashboardSliderRepo
    {
        ApplicationDbContext _context;
        public DashboardSliderRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public ResponseData DeleteData(int id)
        {

            try
            {
                var dbInfo = _context.DashboardSlider.Where(x => x.Id == id).FirstOrDefault();
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

        public ResponseData<List<DashboardSlider>> GetAllData(string SubTitle, string Title, int? Orderkey)
        {
            try
            {
                var query = _context.DashboardSlider.Where(x => x.Status == 1);

                query = query
                    .Where(x => string.IsNullOrEmpty(SubTitle) || x.SubTitle.Contains(SubTitle))
                    .Where(x => string.IsNullOrEmpty(Title) || x.Title.Contains(Title))

                    .Where(x => !Orderkey.HasValue || x.OrderKey == Orderkey.Value);

                var datas = query.ToList();
                return new ResponseData<List<DashboardSlider>>
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

        public ResponseData<DashboardSliderVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .DashboardSlider
                                    .Where(x => x.Id == id)
                                    .Select(s => new DashboardSliderVM
                                    {
                                        Id = s.Id.ToInt32(),
                                        DashboardSliderInfo = s.DashboardSliderInfo.ToText(),
                                        Title = s.Title.ToText(),
                                        SubTitle = s.SubTitle.ToText(),
                                        BackgroundImage = s.BackgroundImage.ToText(),
                                        ValidFrom = s.ValidFrom.ToString(),
                                        ValidTo = s.ValidTo.ToString(),
                                        OrderKey = s.OrderKey.ToInt32()
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<DashboardSliderVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<DashboardSliderVM>
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

        public ResponseData SaveData(DashboardSlider model)
        {
            try
            {
                if (model.Id == 0)
                {
                    _context.DashboardSlider.Add(model);

                    _context.SaveChanges();

                    return new ResponseData
                    {
                        Success = true,
                        Message = "Data Save Success"
                    };
                }
                else
                {
                    var dbInfo = _context.DashboardSlider.Where(x => x.Id == model.Id).FirstOrDefault();
                    if (dbInfo != null)
                    {
                        dbInfo.DashboardSliderInfo = model.DashboardSliderInfo.ToText();
                        dbInfo.Title = model.Title.ToText();
                        dbInfo.SubTitle = model.SubTitle.ToText();
                        dbInfo.BackgroundImage = model.BackgroundImage.ToText();
                        dbInfo.ValidFrom = model.ValidFrom;
                        dbInfo.ValidTo = model.ValidTo;
                        dbInfo.OrderKey = model.OrderKey.ToInt32();
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

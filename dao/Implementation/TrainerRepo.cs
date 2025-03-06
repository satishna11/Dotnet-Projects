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

namespace DAO.Implementation
{
    public class TrainerRepo : ITrainerRepo
    {
        ApplicationDbContext _context;
        public TrainerRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public ResponseData DeleteData(int id)
        {
            try
            {
                var dbInfo = _context.Trainers.Where(x => x.TrainerID == id).FirstOrDefault();
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

        public ResponseData<List<Trainer>> GetAllData(string fname, string designation, string fID)
        {
            try
            {
                var query = _context.Trainers.Where(x => x.Status == 1);

                query = query
                    .Where(x => string.IsNullOrEmpty(fname) || x.FullName.Contains(fname))
                    .Where(x => string.IsNullOrEmpty(designation) || x.Designation.Contains(designation))
                    .Where(x => string.IsNullOrEmpty(fID) || x.Contact.Contains(fID));


                var datas = query.ToList();

                return new ResponseData<List<Trainer>>
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

        public ResponseData<TrainerVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .Trainers
                                    .Where(x => x.TrainerID == id)
                                    .Select(s => new TrainerVM
                                    {
                                        TrainerID = s.TrainerID.ToInt32(),
                                        FullName = s.FullName.ToText(),
                                        Designation = s.Designation.ToText(),
                                        Email = s.Email.ToText(),
                                        Contact = s.Contact.ToText(),
                                        Photo = s.Photo.ToText(),
                                        FacebookID = s.FacebookID.ToText(),
                                        LinkedInID = s.LinkedInID.ToText(),
                                        ShortDescription = s.ShortDescription.ToText(),
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<TrainerVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<TrainerVM>
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

        public ResponseData SaveData(Trainer model)
        {
            try
            {
                if (model.TrainerID == 0)
                {
                    _context.Trainers.Add(model);

                    _context.SaveChanges();

                    return new ResponseData
                    {
                        Success = true,
                        Message = "Data Save Success"
                    };
                }
                else
                {
                    var dbInfo = _context.Trainers.Where(x => x.TrainerID == model.TrainerID).FirstOrDefault();
                    if (dbInfo != null)
                    {
                        dbInfo.FullName = model.FullName.ToText();
                        dbInfo.Designation = model.Designation.ToText();
                        dbInfo.Email = model.Email.ToText();
                        dbInfo.Contact = model.Contact.ToText();
                        dbInfo.Photo = model.Photo.ToText();
                        dbInfo.FacebookID = model.FacebookID.ToText();
                        dbInfo.LinkedInID = model.LinkedInID.ToText();
                        dbInfo.ShortDescription = model.ShortDescription.ToText();
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

using DAO.Interface;
using Entity.Common;
using Entity.Model;
using Entity.ViewModel;
using mcavesServices;
using Microsoft.IdentityModel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Implementation
{
    public class UserGroupRepo : IUserGroupRepo
    {
        ApplicationDbContext _context;
        public UserGroupRepo(ApplicationDbContext context)
        {
            _context = context;
        }


        public ResponseData DeleteData(int id)
        {
            try
            {
                var oldData = _context.UserGroup.Where(x => x.UserGroupID == id).FirstOrDefault();
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
            catch(Exception ex)
            {
                //write exception to filie
                throw ex;
            }
            
        }

        public ResponseData<List<UserGroup>> GetAllData()
        {
            try
            {
                var datas = _context
                                     .UserGroup
                                     .Where(x => x.Status == 1)
                                     .ToList();
                return new ResponseData<List<UserGroup>>
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

        public ResponseData<UserGroupVM> GetDataByID(int id)
        {
            try
            {
                var datas = _context
                                    .UserGroup
                                    .Where(x => x.UserGroupID == id)
                                    .Select(s => new UserGroupVM
                                    {
                                        UserGroupID = s.UserGroupID.ToInt32(),
                                        UserGroupName = s.UserGroupName.ToText(),
                                        UserGroupCode = s.UserGroupCode.ToText(),
                                        ActivateDate = s.ActivateDate.ToNepaliDate()
                                    })
                                    .FirstOrDefault();
                if (datas == null)
                {
                    return new ResponseData<UserGroupVM>()
                    {
                        Success = false,
                        Message = "Data Not Found!"
                    };
                }
                else
                {
                    return new ResponseData<UserGroupVM>
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

        public ResponseData SaveData(UserGroup model)
        {
            try
            {
                _context.UserGroup.Add(model);

                _context.SaveChanges();

                return new ResponseData
                {
                    Success = true,
                    Message = "Data Save Success"
                };
            }
            catch (Exception ex)
            {
                //write ex to a file         // SERILOG
                throw ex;
            }
        }
    }
}

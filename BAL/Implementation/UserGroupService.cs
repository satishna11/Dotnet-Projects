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
    public class UserGroupService : IUserGroupService
    {

        IUserGroupRepo _repo;
        public UserGroupService(IUserGroupRepo repo)
        {
            _repo = repo;
        }


        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<UserGroupVM>> GetAllData()
        {
            var res = _repo.GetAllData();
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new UserGroupVM
                {
                    UserGroupID = s.UserGroupID.ToInt32(),
                    UserGroupName = s.UserGroupName.ToText(),
                    UserGroupCode = s.UserGroupCode.ToText(),
                    ActivateDate = s.ActivateDate.ToNepaliDate()
                }).ToList();

                return new ResponseData<List<UserGroupVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<UserGroupVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<UserGroupVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }

        public ResponseData SaveData(UserGroupVM model)
        {

            if (string.IsNullOrEmpty(model.UserGroupName))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter User Group Name"
                };
            }
            else if (string.IsNullOrEmpty(model.UserGroupCode))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter User Group Code"
                };
            }
            else
            {
                var mdl = new UserGroup();
                mdl.UserGroupName = model.UserGroupName.ToText();
                mdl.UserGroupCode = model.UserGroupCode.ToText();
                mdl.ActivateDate = model.ActivateDate.ToEnglishDate();
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }


    }
}

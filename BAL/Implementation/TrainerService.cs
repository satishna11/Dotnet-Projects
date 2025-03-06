using BAL.Interface;
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

namespace BAL.Implementation
{
    public class TrainerService : ITrainerService
    {
        ITrainerRepo _repo;
        public TrainerService(ITrainerRepo repo)
        {
            _repo = repo;
        }
        public ResponseData DeleteData(int id)
        {
            return _repo.DeleteData(id);
        }

        public ResponseData<List<TrainerVM>> GetAllData(string fname,string designation, string fID)
        {
            var res = _repo.GetAllData(fname,designation,fID);
            if (res.Success)
            {
                var mappedData = res.Data.Select(s => new TrainerVM
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


                }).ToList();

                return new ResponseData<List<TrainerVM>>
                {
                    Success = true,
                    Data = mappedData
                };
            }
            else
            {
                return new ResponseData<List<TrainerVM>>
                {
                    Success = false,
                    Message = res.Message
                };
            }
        }

        public ResponseData<TrainerVM> GetDataByID(int id)
        {
            return _repo.GetDataByID(id);
        }

        

        public ResponseData SaveData(TrainerVM model)
        {
            if (string.IsNullOrEmpty(model.FullName))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Full Name"
                };
            }
            else if (string.IsNullOrEmpty(model.Designation))
            {
                return new ResponseData
                {
                    Success = false,
                    Message = "Enter Designation"
                };
            }
            else
            {
                var mdl = new Trainer();
                mdl.TrainerID = model.TrainerID.ToInt32();
                mdl.FullName = model.FullName.ToText();
                mdl.Designation = model.Designation.ToText();
                mdl.Email = model.Email.ToText();
                mdl.Contact = model.Contact.ToText();
                mdl.Photo = model.Photo.ToText();
                mdl.FacebookID = model.FacebookID.ToText();
                mdl.LinkedInID = model.LinkedInID.ToText();
                mdl.ShortDescription = model.ShortDescription.ToText();
                mdl.Status = 1;
                mdl.CreatedDate = DateTime.Now;
                //mdl.CreatedBy = 

                return _repo.SaveData(mdl);
            }
        }

        ResponseData<TrainerVM> ITrainerService.GetDataByID()
        {
            throw new NotImplementedException();
        }
    }
}

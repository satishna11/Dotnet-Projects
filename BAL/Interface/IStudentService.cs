using System;
using Entity.Common;
using Entity.ViewModel;

namespace BAL.Interface
{
	public interface IStudentService
	{
        ResponseData<List<StudentVM>> GetAllData(string fname,string email,string contact);
        ResponseData<StudentVM> GetDataByID(int id);

        ResponseData SaveData(StudentVM model);

        ResponseData DeleteData(int id);

        ResponseData<StudentVM> GetDataByID();
    }
}


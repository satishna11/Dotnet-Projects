using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Controllers
{
    public class StudentController : Controller
    {
        IStudentService _logic;
        public StudentController(IStudentService logic)
        {
            _logic = logic;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public object SaveData([FromBody] StudentVM model)
        {
            var rsp = _logic.SaveData(model);
            return Ok(rsp);

        }
        [HttpGet]
        public object GetAllData(string fname, string email, string contact)
        {
            var rsp = _logic.GetAllData(fname,email,contact);
            return Ok(rsp);
        }
        [HttpGet]
        public object DeleteData([FromQuery] int id)
        {
            var rsp = (_logic.DeleteData(id));
            return Ok(rsp);
        }
        [HttpGet]
        public object GetDataByID([FromQuery] int id)
        {
            var rsp = _logic.GetDataByID(id);
            return Ok(rsp);
        }
    }
}

using BAL.Interface;
using Entity.Model;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Controllers
{
    public class CourseGroupController : Controller
    {
        ICourseGroupService _logic;
        public CourseGroupController(ICourseGroupService logic)
        {
            _logic = logic;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object SaveData([FromBody] CourseGroupVM model)
        {
            var rsp = _logic.SaveData(model);
            return Ok(rsp);

        }
        [HttpGet]
        public object GetAllData(string Title, string SubTitle)
        {
            var rsp = _logic.GetAllData(Title, SubTitle);
            return Ok(rsp);
        }
        [HttpGet]
        public object DeleteData([FromQuery] int id)
        {
            var rsp = (_logic.DeleteData(id));
            return Ok(rsp);
        }
        [HttpGet]
        public object GetDataByID(int id)
        {

            var rsp = (_logic.GetDataByID(id));

            return Ok(rsp);

        }
    }
}

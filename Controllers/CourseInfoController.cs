using System;
using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Controllers
{
    public class CourseInfoController : Controller
    {
        ICourseInfoService _logic;
        public CourseInfoController(ICourseInfoService logic)
        {
            _logic = logic;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SaveData([FromBody] CourseInfoVM vm)
        {
            try
            {
                var rsp = _logic.SaveData(vm);
                return Ok(rsp);
            }
            catch (Exception ex)
            {
                // Log the exception here (use logging framework like Serilog, NLog, etc.)
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public object GetAllData(string title,int? totalprice)
        {
            var rsp = _logic.GetAllData(title,totalprice);
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
        //public IActionResult GetActiveNotices()
        //{
        //    var currentDate = DateTime.Now;

        //    var activeNotices = _logic.GetNotices()
        //        .Where(n => n.ValidFrom <= currentDate && n.ValidTo >= currentDate)
        //        .ToList();

        //    return Json(activeNotices);
        //}

    }
}

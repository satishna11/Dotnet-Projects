using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Controllers
{
    public class TrainerController : Controller
    {
        ITrainerService _logic;
        public TrainerController(ITrainerService logic)
        {
            _logic = logic;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public object SaveData([FromBody] TrainerVM model)
        {
            var rsp = _logic.SaveData(model);
            return Ok(rsp);

        }
        [HttpGet]
        public object GetAllData(string fname,string designation,string fID)
        {
            var rsp = _logic.GetAllData(fname,designation,fID);
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

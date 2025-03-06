using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ClassManagement.Controllers
{
    public class UserGroupController : Controller
    {
        IUserGroupService _logic;
        public UserGroupController(IUserGroupService logic) 
        {
            _logic = logic;        
        }    

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public object SaveData([FromBody] UserGroupVM vm)
        {
            var rsp = _logic.SaveData(vm);
            return Ok(rsp);
        }

        [HttpGet]
        public object Delete([FromQuery] int id)
        {
            var rsp = _logic.DeleteData(id);
            return Ok(rsp);
        }

        [HttpGet]
        public object GetAllData()
        {
            var rsp = _logic.GetAllData();
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

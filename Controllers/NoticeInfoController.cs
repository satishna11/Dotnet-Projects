using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.Implementation;
using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClassManagement_self.Controllers
{
    public class NoticeInfoController : Controller
    {
        INoticeInfoService _logic;
        public NoticeInfoController(INoticeInfoService logic)
        {
            _logic = logic;
        }


        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public object SaveData([FromBody] NoticeInfoVM vm)
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
        public object GetAllData(string NoticeDetail, string Title, int? Orderkey)

        {
            var rsp = _logic.GetAllData( NoticeDetail,  Title,  Orderkey);
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



using BAL.Interface;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClassManagement_self.Controllers
{
    public class DashboardSliderController : Controller
    {
        IDashboardSliderService _logic;
        public DashboardSliderController(IDashboardSliderService logic)
        {
            _logic = logic;
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public object SaveData([FromBody] DashboardSliderVM model)
        {
            if(!ModelState.IsValid)
    {
                return BadRequest(ModelState); // This will return the validation errors
            }
            // Trim extra whitespace and newline characters
            model.DashboardSliderInfo = model.DashboardSliderInfo?.Trim();
            model.Title = model.Title?.Trim();
            model.SubTitle = model.SubTitle?.Trim();

            var rsp = _logic.SaveData(model);
            return Ok(rsp);
        }


        [HttpGet]
        public object GetAllData(string SubTitle, string Title, int? Orderkey)
        {
            var rsp = _logic.GetAllData(SubTitle,Title,Orderkey);
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

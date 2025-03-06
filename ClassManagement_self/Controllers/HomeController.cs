using BAL.Interface;
using ClassManagement_self.Models;
using Entity.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClassManagement_self.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICourseInfoService _course;
        private IDashboardSliderService _slider;
        private INoticeInfoService _notice;
        public HomeController(ILogger<HomeController> logger, ICourseInfoService course, IDashboardSliderService slider,INoticeInfoService notice)
        {
            _logger = logger;
            _course = course;
            _slider = slider;
            _notice = notice;
        }

        public IActionResult Index()
        {
            DashboardVM vm = new DashboardVM();
            var courseResponse = _course.GetAllData(null,null);
            if (courseResponse.Success)
            {
                vm.courses = courseResponse.Data;
            }

            var  sliderResponse = _slider.GetAllData(null,null,null);
            if (sliderResponse.Success)
            {
                vm.sliders = sliderResponse.Data;
            }
            var noticeRespponse= _notice.GetAllData(null, null,null);
            if (noticeRespponse.Success)
            { 
                vm.notice = noticeRespponse.Data;
            }
            return View(vm);
        }

        //public IActionResult CourseInfo(int id)
        //{
        //    DashboardVM vm = new DashboardVM();
        //    var courseInfo = _course.GetDataByID(id);

        //    if (courseInfo.Success)
        //    {
        //        vm.course = courseInfo.Data;
        //    }
        //    var allcourse = _course.GetAllData(null,null);
        //    if (allcourse.Success)
        //    {
        //        vm.course = courseInfo.Data;
        //    }
        //    return View(vm);
        //}
        public IActionResult CourseInfo(int id)
        {
            DashboardVM vm = new DashboardVM();
            var courseInfo = _course.GetDataByID(id);

            if (courseInfo.Success)
            {
                vm.course = courseInfo.Data;
            }
            else
            {
                // Add logging or an error message to help with debugging
                Console.WriteLine("Error fetching course info: " + courseInfo.Message);
                return StatusCode(500, "Error fetching course info.");
            }

            var allcourse = _course.GetAllData(null, null);
            if (allcourse.Success)
            {
                vm.courses = allcourse.Data; // Assuming you have a property to hold multiple courses
            }
            else
            {
                Console.WriteLine("Error fetching all courses: " + allcourse.Message);
                return StatusCode(500, "Error fetching all courses.");
            }

            return View(vm);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

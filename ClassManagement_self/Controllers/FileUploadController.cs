using Entity.Common;
using Entity.ViewModel;
using Helper;
using mcavesServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace ClassManagement_self.Controllers
{
    public class FileUploadController : Controller
    {
        [HttpPost]
        public JsonResult UploadFile(IFormFile file)
        {
            try
            {
                // Validate file
                if (file == null || file.Length == 0)
                {
                    return Json(new { success = false, message = "Please upload a valid file." });
                }

                // Validate file size (e.g., 2MB limit)
                if (file.Length > 2 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "File size must be less than 2MB." });
                }

                // Validate file type (e.g., only images)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    return Json(new { success = false, message = "Only JPG, JPEG, PNG, and GIF files are allowed." });
                }

                // Save the file to the server
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine("wwwroot/satishna", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Return the file name/path
                return Json(new { success = true, data = fileName });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }
    }
}

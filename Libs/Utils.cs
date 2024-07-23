using hotel_clone_api.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace hotel_clone_api.Libs
{
    public class Utils
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public Utils(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public void DeleteImageFromFolder(string image_name)
        {
            string[] segments = image_name.Split('/');

            string folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            string filePath = Path.Combine(folderPath, segments[segments.Length - 1]);

            if (File.Exists(filePath))
            {
                Console.Write("=======================================================================DELETED");
                File.Delete(filePath);
            }
        }
        public void ValidateFileUpload(List<IFormFile> files, ModelStateDictionary modelState)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            foreach (var file in files)
            {
                if (!allowedExtension.Contains(Path.GetExtension(file.FileName)))
                {
                    modelState.AddModelError("file", "Unsupported file extension");
                }

                if (file.Length > 10485760)
                {
                    modelState.AddModelError("File", "File size more than 10MB, please upload a smaller file");
                }
            }
        }
    }
}

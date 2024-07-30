using hotel_clone_api.Models.DTOs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        public ErrorResponse BuildErrorResponse(ModelStateDictionary modelState)
        {
            var errorResponse = new ErrorResponse
            {
                Status = 400,
                Title = "One or more validation errors occurred.",
                Detail = "Please refer to the errors property for additional details.",
                Errors = modelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    )
            };
            return errorResponse;
        }
    }
}

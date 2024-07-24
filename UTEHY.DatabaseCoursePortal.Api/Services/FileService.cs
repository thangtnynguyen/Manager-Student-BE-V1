using Microsoft.AspNetCore.Mvc;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile? file, string folder)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string relativeFolderPath = folder;
            string uploadsFolder = Path.Combine(webRootPath, relativeFolderPath);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(relativeFolderPath, uniqueFileName);

            using (var stream = new FileStream(Path.Combine(webRootPath, filePath), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string absoluteFilePath = Path.Combine(webRootPath, folder, uniqueFileName);
            string relativeFilePath = Path.GetRelativePath(webRootPath, absoluteFilePath);

            return "/" + relativeFilePath.Replace("\\", "/");
        }

        public async Task<string> DeleteFileAsync(string fileUrl)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, fileUrl.TrimStart('/'));

            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
                return fileUrl;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<string>> DeleteFilesAsync(List<string> fileUrls)
        {
            List<string> deletedUrls = new List<string>();

            foreach (var fileUrl in fileUrls)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string filePath = Path.Combine(webRootPath, fileUrl.TrimStart('/'));

                if (File.Exists(filePath))
                {
                    await Task.Run(() => File.Delete(filePath));
                    deletedUrls.Add(fileUrl);
                }
            }

            return deletedUrls;
        }
    }
}

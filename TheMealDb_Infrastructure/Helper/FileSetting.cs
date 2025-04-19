
using Microsoft.AspNetCore.Http;

namespace Landing.PL.Helpers
{
    public class FileSetting
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
           
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files", folderName);
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            string filePath=Path.Combine(folderPath,fileName);
            var fileStream = new FileStream(filePath,FileMode.Create);
            file.CopyTo(fileStream);
            return fileName ;
        }

        public static void DeleteFile(string fileName,string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName,fileName);
            if (File.Exists(filePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(filePath);
            }
        }
    }
}


using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace Landing.PL.Helpers
{
    public class FileSetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", folderName);

            // حساب الـ hash من الصورة
            using var ms = new MemoryStream();
            file.CopyTo(ms);
            byte[] fileBytes = ms.ToArray();

            using var sha = SHA256.Create(); // أو MD5.Create()
            var hashBytes = sha.ComputeHash(fileBytes);
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // استخراج امتداد الملف
            var fileExtension = Path.GetExtension(file.FileName);

            // اسم الملف يكون هو الـ hash + الامتداد (لتمييز الصور بنفس الاسم لكن مختلفة)
            string fileName = $"{hashString}{fileExtension}";
            string filePath = Path.Combine(folderPath, fileName);

            // إذا كانت الصورة موجودة لا ترفع من جديد
            if (!File.Exists(filePath))
            {
                File.WriteAllBytes(filePath, fileBytes); // نحفظ الصورة من البايتات مباشرة
            }

            return fileName;
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

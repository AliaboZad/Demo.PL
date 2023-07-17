using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo.PL.Helpers
{
    public static class DecumentSetting
    {
        public static string UploadFile(IFormFile file , string folderName )
        {
            //1. Get Located Folder path 
            //string folderpath = "C:\\Users\\aliom\\source\\repos\\Demo.PL\\Demo.PL\\wwwroot\\Files\\";

            string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", folderName);

            // 2. Get file Name 
            // we use (Guid.NewGuid()) to Make the Name of file is Uniqe 
            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. File Path 
            string filepathe = Path.Combine(folderpath, fileName);

            //4. Save file as Streams : [Data per Time]
            //we use (Using) to close the file Connection 
            using var filestream = new FileStream(filepathe, FileMode.Create);
            file.CopyTo(filestream);
            return fileName;
            
        }

        public static void DeletFile(string fileName , string folderName )
        {
            if(fileName is not null && folderName is not null )
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory (), "wwwroot\\Files",folderName ,fileName );
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
        }
    }
}

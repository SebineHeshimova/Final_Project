using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Business.Extensions
{
    public static class Helper
    {
        public static string SaveFile(string rootPath, string folderName, IFormFile imageFile)
        {
            string file = imageFile.FileName.Length > 64 ? imageFile.FileName.Substring(imageFile.FileName.Length - 64, 64) : imageFile.FileName;
            file=Guid.NewGuid() + file;
            string path=Path.Combine(rootPath, folderName, file);
            using(FileStream stream=new FileStream(path, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return file;
        }
        public static void DeleteFile(string rootPath, string folderName, string imageUrl)
        {
            string deletePath= Path.Combine(rootPath, folderName, imageUrl);
            if(System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
        }
    }

}

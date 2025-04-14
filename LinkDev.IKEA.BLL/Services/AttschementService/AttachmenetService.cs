using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.AttschementService
{
    public class AttachmenetService : IAttachmenetService
    {
        public string? Upload(IFormFile file, string FolderName)
        {
            List<string> AllowedExtensions = [".png", ".jpg", ".jepg"];
            const int MaxSize = 2_097_152;

            #region 1. Check Extension 

            var extension = Path.GetExtension(file.FileName);

            if (!AllowedExtensions.Contains(extension))
                return null;
            #endregion

            #region 2. Check Size 

            if (file.Length<=0||file.Length>MaxSize)
                return null;
            #endregion

            #region 3. Get Located Folder Path

            var FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Files", FolderName);
            #endregion

            #region 4. Make Attachment Name Unique -- GUID

            var FileName = $"{Guid.NewGuid()}_{file.FileName}";
            #endregion

            #region 5. Get File Path
            var FilePath = Path.Combine(FolderPath, FileName);

            #endregion

            #region 6. Create File Stream To Copy File [Unmanaged]
            using var fs = new FileStream(FilePath, FileMode.Create);
            #endregion

            #region 7. Use Stream To Copy File 
            file.CopyTo(fs);
            #endregion

            #region 8. Return FileName To Store In Database 

            return FileName; 
            #endregion

        }

        public bool Delete(string FilePath)
        {
            if (!File.Exists(FilePath))
                return false;
            File.Delete(FilePath);
            return true;
        }

      
    }
}

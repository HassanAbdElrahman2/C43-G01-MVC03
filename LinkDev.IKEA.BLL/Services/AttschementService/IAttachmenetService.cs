using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Services.AttschementService
{
    public interface IAttachmenetService
    {
        public string? Upload(IFormFile file,string FolderName);
        public bool Delete(string FilePath);
    }
}

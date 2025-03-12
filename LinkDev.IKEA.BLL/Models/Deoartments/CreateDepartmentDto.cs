using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Deoartments
{
    public record CreateDepartmentDto(string Name, string? Description, string Code, DateOnly CreationDate);
}

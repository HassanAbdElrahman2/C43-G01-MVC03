using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BLL.Models.Deoartments
{
    public record DepartmentDetailsDto(int Id, string CreatedBy, DateTime CreatedOn, string LastModifiedBy, DateTime LastModifiedOn, string Name, string Code, string? Description, DateOnly CreationDate);
}

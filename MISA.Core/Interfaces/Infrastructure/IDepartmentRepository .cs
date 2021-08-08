
using MISA.Core.Entities;
using MISA.Core.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Interfaces
{
    public interface IDepartmentRepository:IBaseRepository<Department>
    {

        public bool CheckDuplicateDepartmentCode(string departmentCode);
    }
}

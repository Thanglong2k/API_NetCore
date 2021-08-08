using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.Infarstructure
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #region Methods

        /// <summary>
        /// Hàm kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true- đã tồn tại; false- chưa tồn tại</returns>
        /// CreatedBy: TTLONG (26/7/2021)
        public bool CheckDuplicateDepartmentCode(string departmentCode)
        {
            var isDuplicate = false;

            var sqlCommand = $"SELECT DepartmentCode FROM Department where DepartmentCode = @DepartmentCode";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@DepartmentCode", departmentCode);

            //Lấy dữ liệu với Dapper:
            var departments = DbConnection.QueryFirstOrDefault<string>(sql: sqlCommand, param: paramaters);
            if (departments != null)
                isDuplicate = true;

            return isDuplicate;
        }
        #endregion
    }
}

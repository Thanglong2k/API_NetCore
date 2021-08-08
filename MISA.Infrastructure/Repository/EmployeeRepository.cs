using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.Infrastructure.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns>true/false</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public bool CheckDuplicateEmployeeCode(string employeeCode)
        {
          

            var sqlCommand = $"Select exists(SELECT EmployeeCode FROM Employee where EmployeeCode = @EmployeeCode)";
            DynamicParameters paramaters = new DynamicParameters();
            paramaters.Add("@EmployeeCode", employeeCode);

            //Lấy dữ liệu với Dapper:
            var isDuplicate = DbConnection.QueryFirstOrDefault<bool>(sql: sqlCommand, param: paramaters);
            

            return isDuplicate;
        }
        /// <summary>
        /// Lọc dữ liệu
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        public object GetFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int pageNumber, int pageSize)
        {
            try
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"EmployeeFilter", employeeFilter);
                parameters.Add($"DepartmentId", departmentId);
                parameters.Add($"PositionId", positionId);
                parameters.Add($"PageNumber", pageNumber);
                parameters.Add($"PageSize", pageSize);
                parameters.Add($"TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add($"TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                //Khởi tạo các CommandText:
                var entitites = DbConnection.Query<Employee>("Proc_GetEmployeesFilterPaging", parameters, commandType: CommandType.StoredProcedure);
                var output = new OutputFilterPaging();
                output.Data = entitites;
                output.TotalRecord = parameters.Get<int>("TotalRecord");
                output.TotalPage = parameters.Get<int>("TotalPage");
                //Trả về dữ liệu:
                return output;

            }
            catch (Exception e)
            {
                var t = e.Message;

                return null;
            }
        }
    }
}

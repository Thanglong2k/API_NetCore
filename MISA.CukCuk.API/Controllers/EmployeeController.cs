using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using System.Data;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using MISA.Core.Const;
using Microsoft.AspNetCore.Cors;

namespace MISA.CukCuk.API.Controllers
{
   /* [EnableCors("AllowOrigin")]*/
    [Route("api/v1/employees")]
    [ApiController]
    public class EmployeeController : BaseEntityController<Employee>
    {
        #region Declare
        IEmployeeService _employeeService;
        #endregion
        #region Constructor
        public EmployeeController(IEmployeeService employeeService):base(employeeService)
        {
            _employeeService = employeeService;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lọc và phân trang
        /// </summary>
        /// <param name="employeeFilter">Dữ liệu tìm kiếm theo mã, tên</param>
        /// <param name="departmentId">id phòng ban</param>
        /// <param name="positionId">id vị trí</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="pageSize">số bản ghi 1 trang</param>
        /// <returns>Đối tượng lưu trữ dữ liệu</returns>
        /// CreatedBy: TTLONG (28/07/2021)
        [HttpGet("employeeFilter")]

        public IActionResult GetFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int pageNumber, int pageSize)
        {
            try
            {
                employeeFilter = employeeFilter == null ? "" : employeeFilter;
                var serviceResult = _employeeService.GetFilterPaging(employeeFilter, departmentId, positionId, pageNumber, pageSize);

                if (serviceResult.Data != null)
                    return Ok(serviceResult.Data);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return base.StatusCode(500, ServiceResult);
            }
        }
        #endregion
    }

}

using MISA.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.Core.Interfaces.Service
{
    public interface IEmployeeService:IBaseService<Employee>
    {

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
        ServiceResult GetFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int startRecordPaging, int pageSize);
    }
}


using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


using MISA.Core.Const;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/v1/departments")]
    [ApiController]
    public class DepartmentController : BaseEntityController<Department>
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>
        private ResponseError _responseError;
        private IDepartmentService _departmentService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public DepartmentController(IDepartmentService departmentService):base(departmentService)
        {
            _responseError = new ResponseError();
            _departmentService = departmentService;
        }
        #endregion
        /**
         * 200: OK
         * 201: Thêm mới thành công dữ liệu vào DB
         * 204: Không có dữ liệu
         * 400: BadRequest: Dữ liệu đầu vào từ Client ko hợp lệ
         * 404: Không tìm thấy resource phù hợp
         * 500: Lỗi phía server
        */
        /*#region Method
        /// <summary>
        /// Lấy tất cả phòng ban
        /// </summary>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {

                var serviceResult = _departmentService.GetAll();
                var tg = serviceResult.Data;
                if (serviceResult.Data != null)
                    return Ok(serviceResult);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                _responseError.DevMsg = e.Message;
                _responseError.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                _responseError.ErrorCode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, _responseError);
            }
        }
        /// <summary>
        /// Lấy phòng ban theo Id
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet("{departmentId}")]
        public IActionResult GetById(Guid departmentId)
        {
            try
            {
                var serviceResult = _departmentService.GetById(departmentId);

                if (serviceResult.Data != null)
                    return Ok(serviceResult);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                _responseError.DevMsg = null;
                _responseError.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                _responseError.ErrorCode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, _responseError);
            }
        }
        /// <summary>
        /// Xóa phòng ban
        /// </summary>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpDelete("{departmentId}")]
        public IActionResult Delete(Guid departmentId)
        {
            try
            {
                var serviceResult = _departmentService.Delete(departmentId);
                var rowAffect = (int)serviceResult.Data;
                if (rowAffect > 0)
                {
                    return Ok(rowAffect);
                }
                return NoContent();
            }

            catch (Exception e)
            {
                _responseError.DevMsg = e.Message;
                _responseError.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                _responseError.ErrorCode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, _responseError);
            }
        }
        /// <summary>
        /// Cập nhật phòng ban
        /// </summary>
        /// <param name="department">THông tin phòng ban</param>
        /// <param name="departmentId">Id phòng ban</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPut("{departmentId}")]
        public IActionResult Update([FromBody] Department department, Guid departmentId)
        {
            try
            {
                var serviceResult = _departmentService.Update(department, departmentId);

                var rowAffect = (int)serviceResult.Data;
                if (rowAffect > 0)
                {
                    return StatusCode(200, rowAffect);
                }
                return NoContent();
                //Lấy dữ liệu với Dapper:

            }
            catch (Exception e)
            {
                var t = e.Message;
                _responseError.ErrorCode = MisaConst.MISACodeErrorException;
                _responseError.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                _responseError.DevMsg = e.Message;
                return StatusCode(500, _responseError);
            }

        }
        /// <summary>
        /// Thêm phòng ban
        /// </summary>
        /// <param name="department">THông tin phòng ban</param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPost]
        public IActionResult Post([FromBody] Department department)
        {

            try
            {
                var serviceResult = _departmentService.Add(department);

                var rowAffect = serviceResult.Data;
                if (rowAffect != null)
                {
                    return StatusCode(201, (int)rowAffect);
                }
                _responseError.ErrorCode = serviceResult.MISACode;
                _responseError.UserMsg = serviceResult.UserMsg;
                _responseError.DevMsg = serviceResult.DevMsg;
                return StatusCode(400, _responseError);
                //Lấy dữ liệu với Dapper:

            }
            catch (Exception e)
            {
                var t = e.Message;
                _responseError.ErrorCode = MisaConst.MISACodeErrorException;
                _responseError.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                _responseError.DevMsg = e.Message;
                return StatusCode(500, _responseError);
            }

        }
        #endregion*/
    }
}

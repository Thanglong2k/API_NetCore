using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Misa.ApplicationCore.Service;


using MISA.Core.Const;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/positions")]
    [ApiController]
    public class PositionController : BaseEntityController<Position>
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>
        private ResponseError _responseError;
        private IPositionService _positionService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public PositionController(IPositionService positionService):base(positionService)
        {
            _responseError = new ResponseError();
            _positionService = positionService;
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
        /*#region Methods
        /// <summary>
        /// Lấy tất cả vị trí
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

                var serviceResult = _positionService.GetAll();
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
        /// Lấy vị trí theo Id
        /// </summary>
        /// <param name="PositionId">Id vị trí</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet("{positionId}")]
        public IActionResult GetById(Guid positionId)
        {
            try
            {
                var serviceResult = _positionService.GetById(positionId);

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
        /// Xóa vị trí
        /// </summary>
        /// <param name="PositionId">Id vị trí</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpDelete("{PositionId}")]
        public IActionResult Delete(Guid PositionId)
        {
            try
            {
                var serviceResult = _positionService.Delete(PositionId);
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
        /// Cập nhật vị trí
        /// </summary>
        /// <param name="Position">THông tin vị trí</param>
        /// <param name="PositionId">Id vị trí</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPut("{PositionId}")]
        public IActionResult Update([FromBody] Position position, Guid positionId)
        {
            try
            {
                var serviceResult = _positionService.Update(position, positionId);

                var rowAffect = (int)serviceResult.Data;
                if (rowAffect > 0)
                {
                    return StatusCode(201, rowAffect);
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
        /// Thêm vị trí
        /// </summary>
        /// <param name="position">THông tin vị trí</param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPost]
        public IActionResult Post([FromBody] Position position)
        {

            try
            {
                var serviceResult = _positionService.Add(position);

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

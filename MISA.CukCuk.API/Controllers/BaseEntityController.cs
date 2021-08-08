using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Core.Const;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseEntityController<MISAEntity> : ControllerBase
    {
        #region Field
        /// <summary>
        /// Đối tượng chứa thông tin lỗi
        /// </summary>
        
        protected ServiceResult ServiceResult;
        private IBaseService<MISAEntity> _baseService;
        #endregion
        #region Constructor
        /// <summary>
        /// Khởi tạo các thông tin cần thiết
        /// </summary>
        public BaseEntityController(IBaseService<MISAEntity> baseService)
        {
            
            ServiceResult = new ServiceResult();
            _baseService = baseService;
        }
        #endregion
        #region Methods

        /// <summary>
        /// Lấy tất cả đối tượng
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

                ServiceResult = _baseService.GetAll();
                var tg = ServiceResult.Data;
                if (ServiceResult.Data != null)
                    return Ok(ServiceResult);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;
                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Lấy đối tượng theo Id
        /// </summary>
        /// <param name="id">Id đối tượng</param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                ServiceResult = _baseService.GetById(id);

                if (ServiceResult.Data != null)
                    return Ok(ServiceResult);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        [HttpGet("NewCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                ServiceResult = _baseService.GetNewCode();

                if (ServiceResult.Data != null)
                    return Ok(ServiceResult);//StatusCode(200,"MISA")
                return NoContent();
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        
        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// 200 - thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (25/7/2021)
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                ServiceResult = _baseService.Delete(id);
                var result = ServiceResult.Data;
                if (result !=null)
                {
                    return Ok(ServiceResult);
                }
                
                return StatusCode(400, ServiceResult);
            }

            catch (Exception e)
            {
                ServiceResult.Success = false;

                ServiceResult.DevMsg = e.Message;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                var t = e.Message;
                return StatusCode(500, ServiceResult);
            }
        }
        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPost]
        public IActionResult Post([FromBody] MISAEntity entity)
        {

            try
            {
                ServiceResult = _baseService.Add(entity);

                var result = ServiceResult.Success;
                if (result)
                {
                    return StatusCode(201, ServiceResult);
                }
                
                return BadRequest(ServiceResult);
                //Lấy dữ liệu với Dapper:

            }
            catch (Exception e)
            {
                var t = e.Message;
                ServiceResult.Success = false;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.DevMsg = e.Message;
                return StatusCode(500, ServiceResult);
            }

        }
        /// <summary>
        /// cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng đối tượng</param>
        /// <param name="id">Id đối tượng</param>
        /// <returns>
        /// 200 - thành công
        /// 201 - thêm thành công
        /// 400 - dữ liệu đầu vào không hợp lệ
        /// 500 - Exception
        /// </returns>
        /// CreatedBy: TTLONG (26/7/2021)
        [HttpPut("id")]
        public IActionResult Update([FromBody] MISAEntity entity, Guid id)
        {

            try
            {
                ServiceResult = _baseService.Update(entity, id);

                var result = ServiceResult.Data;
                if (result != null)
                {
                    return StatusCode(200, ServiceResult);
                }
                return BadRequest(ServiceResult);
                //Lấy dữ liệu với Dapper:

            }
            catch (Exception e)
            {
                var t = e.Message;
                ServiceResult.MISACode = MisaConst.MISACodeErrorException;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.ErrorException;
                ServiceResult.DevMsg = e.Message;
                return StatusCode(500, ServiceResult);
            }

        }
        #endregion
    }
}



using MISA.Core.Const;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.ApplicationCore.Service
{
    public class PositionService:IPositionService
    {
        #region Declare
        private ResponseError _responseError;
        public ServiceResult ServiceResult;
        IPositionRepository _positionRepository;
        #endregion
        #region Constructor
        public PositionService(IPositionRepository positionRepository)
        {
            ServiceResult = new ServiceResult();
            _responseError = new ResponseError();
            _positionRepository = positionRepository;
        }
        #endregion
        #region Method
        /// <summary>
        /// Lấy tát cả vị trí
        /// </summary>
        /// <returns>
        /// Danh sách vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult GetAll()
        {
            ServiceResult.Data = _positionRepository.GetAll();
            return ServiceResult;
        }
        /// <summary>
        /// Lấy vị trí theo Id
        /// </summary>
        /// <param name="positionId"></param>
        /// <returns>
        /// THông tin vị trí đưuọc lấy theo Id
        /// </returns>
        /// CreatedBy: TTLONG (28/7/2021)
        public ServiceResult GetById(Guid positionId)
        {

            ServiceResult.Data = _positionRepository.GetById(positionId);
            return ServiceResult;
        }
        //THêm mới vị trí:
        /// <summary>
        /// Service thêm mới vị trí
        /// </summary>
        /// <param name="position"> đối tượng vị trí</param>
        /// <returns>
        /// Kết quả thêm vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Add(Position position)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã vị trí chưa?:
            if (string.IsNullOrEmpty(position.PositionCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = MisaConst.MISACodeEmpty;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //2. Mã vị trí có trùng hay không? - Không được phép để trùng
            if (_positionRepository.CheckDuplicatePositionCode(position.PositionCode))
            {
                ServiceResult.Success = false;
                ServiceResult.MISACode = MisaConst.MISACodeDuplicate;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.Msg_Duplicated;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }
            //3. Email có đúng định dạng hay không?
            position.PositionId = new Guid();
            ServiceResult.Data = _positionRepository.Add(position);
            return ServiceResult;
            
        }
        //Sửa vị trí:
        /// <summary>
        /// Service cập nhật vị trí
        /// </summary>
        /// <param name="position"> đối tượng vị trí</param>
        /// <param name="positionId"> Id vị trí</param>
        /// <returns>
        /// Kết quả cập nhật vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Update(Position position, Guid positionId)
        {

            //validate dữ liệu:
            //1. Kiểm tta xem đã có thông tin mã vị trí chưa?:
            if (string.IsNullOrEmpty(position.PositionCode))
            {

                ServiceResult.Success = false;
                ServiceResult.MISACode = MisaConst.MISACodeEmpty;
                ServiceResult.UserMsg = MISA.Core.Properties.Resources.Msg_Required;
                //serviceResult.Data = _responseError;
                return ServiceResult;
            }

            //3. Email có đúng định dạng hay không?

            ServiceResult.Data = _positionRepository.Update(position, positionId);
            return ServiceResult;
            
        }
        //Xóa vị trí
        /// <summary>
        /// Service xóa vị trí
        /// </summary>
        /// <param name="positionId"> Id vị trí</param>
        /// <returns>
        /// Kết quả xóa vị trí
        /// </returns>
        /// CreatedBy: TTLONG (29/7/2021)
        public ServiceResult Delete(Guid positionId)
        {

            ServiceResult.Data = _positionRepository.Delete(positionId);
            return ServiceResult;
        }
        public ServiceResult GetNewCode()
        {

            ServiceResult.Data = _positionRepository.GetNewCode();
            return ServiceResult;
        }
        #endregion
    }
}

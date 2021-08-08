
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MISA.Core.Const;
using MISA.Core.Entities;
using MISA.Core.Interfaces;
using MISA.Core.Interfaces.Service;
using static MISA.Core.Entities.BaseEntity;
using static MISA.Core.Entities.Employee;

namespace MISA.Core.Services
{
    public class EmployeeService:BaseService<Employee>,IEmployeeService
    {
        #region Declare
        
        //public ServiceResult ServiceResult;
        private IEmployeeRepository _employeeRepository;
        #endregion
        #region Constructor
        public EmployeeService(IEmployeeRepository employeeRepossitory):base(employeeRepossitory)
        {
            //ServiceResult = new ServiceResult();
            
            _employeeRepository = employeeRepossitory;
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
        public ServiceResult GetFilterPaging(string employeeFilter, Guid? departmentId, Guid? positionId, int pageNumber, int pageSize)
        {
            ServiceResult.Data = _employeeRepository.GetFilterPaging(employeeFilter, departmentId, positionId, pageNumber, pageSize);
            return ServiceResult;
        }
       
        /// <summary>
        /// Validate tùy chỉnh theo màn hình nhân viên
        /// </summary>
        /// <param name="entity">Thực thể nhân viên</param>
        /// <returns>(true-false)</returns>
        protected override bool ValidateCustom(Employee employee)
        {
            bool isValid = true;

            //1. Đọc các property
            var properties = employee.GetType().GetProperties();

            foreach (var property in properties)
            {

                var a = property.IsDefined(typeof(AEmail), false);
                if (isValid && property.IsDefined(typeof(AEmail), false))
                {
                    //1.2 Kiểm tra định dạng email
                    isValid = ValidateEmail(employee, property);
                }
                if (isValid && property.IsDefined(typeof(APhoneNumber), false))
                {
                    //1.2 Kiểm tra định dạng số điện thoại
                    var regex= @"^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
                    isValid = ValidateProperty(employee, property, regex);
                }
                if (isValid && property.IsDefined(typeof(Date), false))
                {
                    //1.2 Kiểm tra tuổi
                    
                    if (property.Name=="DateOfBirth")
                    {
                        TimeSpan time = DateTime.Now - Convert.ToDateTime(property.GetValue(employee));
                        if (Math.Ceiling((double)time.Days/365) < 18)
                        {
                            isValid = false;
                            ServiceResult.Data = property.Name;
                            ServiceResult.MISACode = MISA.Core.Const.MisaConst.MISACodeValidate;
                            ServiceResult.UserMsg = string.Format(MISA.Core.Properties.Resources.Msg_DateOfBirth);
                            ServiceResult.Success = false;
                        }
                        
                    }
                    else
                    {
                        
                        if (DateTime.Now < Convert.ToDateTime(property.GetValue(employee)))
                        {
                            isValid = false;
                            ServiceResult.Data = property.Name;
                            ServiceResult.MISACode = MISA.Core.Const.MisaConst.MISACodeValidate;
                            ServiceResult.UserMsg = string.Format(MISA.Core.Properties.Resources.Msg_IdentityDate);
                            ServiceResult.Success = false;
                        }
                    }

                }
                if (isValid && property.Name== "IdentityNumber")
                {
                    //1.2 Kiểm tra định dạng số điện thoại
                    var regex = @"^[0-9]{12}$";
                    isValid = ValidateProperty(employee, property, regex);
                }
            }
            return isValid;
        }
        /// <summary>
        /// Validate định dạng email
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="propertyInfo"></param>
        /// <returns>(true-đúng false-sai)</returns>
        /// CREATED BY: DVHAI (07/07/2021)
        private bool ValidateEmail(Employee employee, PropertyInfo propertyInfo)
        {
            bool isValid = true;

            //1. Tên trường
            var propertyName = propertyInfo.Name;

            //2. Tên hiển thị
            var propertyDisplayName = propertyInfo.GetCustomAttribute(typeof(DisplayName), true);

            //3. Gía trị
            var value = propertyInfo.GetValue(employee);

            //Không validate required
            if (string.IsNullOrEmpty(value.ToString()))
                return isValid;


            isValid = new EmailAddressAttribute().IsValid(value.ToString());

            if (!isValid)
            {
                ServiceResult.Data = propertyInfo.Name;
                ServiceResult.MISACode = MISA.Core.Const.MisaConst.MISACodeValidate;
                ServiceResult.UserMsg = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
                ServiceResult.Data = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
            }

            return isValid;
        }
        /// <summary>
        /// Validate định dạng thuộc tính theo regex
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="propertyInfo"></param>
        /// <returns>(true-đúng false-sai)</returns>
        /// CreatedBy: TTLONG (04/08/2021)
        private bool ValidateProperty(Employee employee, PropertyInfo propertyInfo, string regex)
        {
            var isValid = true;

            //1. Tên trường
            var propertyName = propertyInfo.Name;

            //2. Tên hiển thị
            var propertyDisplayName = propertyInfo.GetCustomAttribute(typeof(DisplayName), true);

            //3. Gía trị
            var value = propertyInfo.GetValue(employee);

            //Không validate required
            if (string.IsNullOrEmpty(value.ToString()))
                return isValid;

            //var regex = @"^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                isValid = false;
            }
            if (!isValid)
            {
                ServiceResult.Data = propertyInfo.Name;
                ServiceResult.MISACode = MISA.Core.Const.MisaConst.MISACodeValidate;
                ServiceResult.UserMsg = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
                ServiceResult.Data = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
            }

            return isValid;
        }
        /// <summary>
        /// Validate định dạng thuộc tính theo regex
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="propertyInfo"></param>
        /// <returns>(true-đúng false-sai)</returns>
        /// CreatedBy: TTLONG (04/08/2021)
        private bool ValidateAge(Employee employee, PropertyInfo propertyInfo, string regex)
        {
            var isValid = true;

            //1. Tên trường
            var propertyName = propertyInfo.Name;

            //2. Tên hiển thị
            var propertyDisplayName = propertyInfo.GetCustomAttribute(typeof(DisplayName), true);

            //3. Gía trị
            var value = propertyInfo.GetValue(employee);

            //Không validate required
            if (string.IsNullOrEmpty(value.ToString()))
                return isValid;

            //var regex = @"^(0|(\+84)){1}(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$";
            var match = Regex.Match(value.ToString(), regex, RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                isValid = false;
            }
            if (!isValid)
            {
                ServiceResult.MISACode = MISA.Core.Const.MisaConst.MISACodeValidate;
                ServiceResult.UserMsg = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
                ServiceResult.Data = string.Format(MISA.Core.Properties.Resources.Msg_NotFormat, (propertyDisplayName as DisplayName).Name);
            }

            return isValid;
        }
        #endregion
    }
}

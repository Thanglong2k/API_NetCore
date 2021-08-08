

using MISA.Core.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Core.Entities
{
    /// <summary>
    /// Thông tin nhân viên
    /// CreatedBy TTLONG (23/7/2021)
    /// </summary>
    public class Employee : BaseEntity
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class Date : Attribute
        {

        }
        #region Property
        /// <summary>
        /// Khóa chính
        /// </summary>
        [PrimaryKey]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [Duplicate]
        [Required]
        [DisplayName("Mã khách hàng")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// HỌ và tên
        /// </summary>
        [Required]
        [DisplayName("Họ và tên")]
        public string FullName { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }
        /// <summary>
        /// Họ
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Tên
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        [Date]
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Duplicate]
        [Required]
        [DisplayName("Số điện thoại")]
        [APhoneNumber]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// email
        /// </summary>
        [Duplicate]
        [Required]
        [DisplayName("Email")]
        [AEmail]
        public string Email { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }
        /// <summary> 
        /// Số CMND/CCCD
        /// </summary>
        [Duplicate]
        //[RangeLength(9,12,"Số CMND/CCCD phải từ 9-12 số")]
        [Required]
        [DisplayName("Số CMND/CCCD")]
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp CMND/CCCD
        /// </summary>
        [Date]

        public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp CMND/CCCD
        /// </summary>
        public string IdentityPlace { get; set; }
        /// <summary>
        /// Ngày gia nhập công ty
        /// </summary>
        [Date]

        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// Tình trạng hôn nhân
        /// </summary>
        public int MartialStatus { get; set; }
        /// <summary>
        /// Học vấn
        /// </summary>
        public int EducationalBackground { get; set; }
        /// <summary>
        /// Id Trình độ chuyên môn
        /// </summary>
        public Guid? QualificationId { get; set; }
        /// <summary>
        /// Id Phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// Id Vị trí
        /// </summary>
        public Guid? PositionId { get; set; }
        /// <summary>
        /// TÌnh trạng làm việc
        /// </summary>
        public WorkStatus? WorkStatus { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        public string PersonalTaxCode { get; set; }
        /// <summary>
        /// Lương
        /// </summary>
        public string Salary { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// Tên vị trí
        /// </summary>
        public string PositionName { get; set; }
        /// <summary>
        /// Tên giới tính
        /// </summary>
        public string GenderName {
            get
            {
                var gender = "";
                if (Gender == Enum.Gender.Male) gender = "Nam";
                if (Gender == Enum.Gender.Female) gender = "Nữ";
                if (Gender == Enum.Gender.Other) gender = "Không xác định";
                return gender;
            }
        }
        /// <summary>
        /// Tên trạng thái làm việc
        /// </summary>
        public string WorkStatusName
        {
            get
            {
                var workStatus = "";
                if (WorkStatus == Enum.WorkStatus.Working) workStatus = "Đang làm việc";
                if (WorkStatus == Enum.WorkStatus.Retired) workStatus = "Đã nghỉ làm";
                if (WorkStatus == Enum.WorkStatus.Other) workStatus = "Khác";
                return workStatus;
            }
        }
        /*/// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }*/
        #endregion

    }
}
